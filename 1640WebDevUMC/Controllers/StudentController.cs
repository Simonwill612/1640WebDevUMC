using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;

public class StudentController : Controller
{
    private readonly EmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public StudentController(EmailService emailService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
        _emailService = emailService;

    }

    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return NotFound();
        }

        var userFaculties = _context.Faculties.Where(f => f.Users.Any(u => u.Id == currentUser.Id)).Select(f => f.FacultyName).ToList();

        IQueryable<Contribution> contributionsQuery = _context.Contributions
            .Include(c => c.AcademicYear)
                .ThenInclude(a => a.Faculty)
            .Include(c => c.Comments)
            .Include(c => c.ApplicationUser)
            .Where(c => userFaculties.Contains(c.AcademicYear.Faculty.FacultyName) || c.IsPublic);

        var contributions = await contributionsQuery.ToListAsync();

        return View(contributions);
    }

    public async Task<IActionResult> Upload(string id)
    {
        if (id == null)
        {
            return BadRequest(); // Return 400 Bad Request if id is null
        }

        var contribution = await _context.Contributions.FindAsync(id);
        if (contribution == null)
        {
            return NotFound(); // Return 404 Not Found if contribution is not found
        }

        return View(contribution);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(string id, List<IFormFile> files, string uploadTitle)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["ErrorMessage"] = "Bạn cần đăng nhập để tải lên file.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            TempData["ErrorMessage"] = "Vui lòng xác nhận email của bạn trước khi tải lên file.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (!await _userManager.IsInRoleAsync(user, "Student"))
        {
            TempData["ErrorMessage"] = "Chỉ sinh viên được phép tải lên file.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (files == null || files.Count == 0)
        {
            TempData["ErrorMessage"] = "Vui lòng chọn file để tải lên.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        var userFiles = await _context.Files.Where(f => f.StudentEmail == user.Email && f.ContributionID == id).ToListAsync();
        var userUploadedFilesCount = userFiles.Count(f => f.ContributionID == id);

        if (userUploadedFilesCount >= 2)
        {
            TempData["ErrorMessage"] = "Bạn đã tải lên tối đa 2 file cho đóng góp này.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (string.IsNullOrEmpty(id))
        {
            TempData["ErrorMessage"] = "ID của đóng góp không hợp lệ.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        var contributionEntity = await _context.Contributions.FindAsync(id);
        if (contributionEntity == null)
        {
            TempData["ErrorMessage"] = "Không tìm thấy đóng góp.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (contributionEntity.IsPublic)
        {
            TempData["ErrorMessage"] = "Bài viết này được công khai, bạn không thể tải lên file.";
            return RedirectToAction("Details", "Student", new { id = id });
        }

        var existingNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.ContributionID == id);
        if (existingNotification == null)
        {
            var newNotification = new Notification
            {
                ContributionID = id,
                Message = $"Xin vui lòng comment về file của sinh viên trong vòng 14 ngày.",
                DueDate = DateTime.Now.AddDays(14)
            };

            _context.Notifications.Add(newNotification);
        }

        var academicYear = await _context.AcademicYears.FindAsync(contributionEntity.AcademicYearID);
        if (DateTime.Now > academicYear.FinalClosureDate)
        {
            TempData["ErrorMessage"] = "Đã hết hạn. Bạn không thể tải lên file.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        try
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                var folder = extension == ".pdf" ? "files" : "images";
                var directoryPath = Path.Combine("wwwroot", folder, contributionEntity.ContributionID);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}{extension}";
                var filePath = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativeFilePath = $"/{folder}/{contributionEntity.ContributionID}/{fileName}";

                var uploadedFile = new _1640WebDevUMC.Models.File
                {
                    ContributionID = contributionEntity.ContributionID,
                    FileName = fileName,
                    FilePath = relativeFilePath,
                    StudentEmail = user.Email,
                    UploadTitle = uploadTitle,
                    UploadTime = DateTime.Now
                };

                _context.Files.Add(uploadedFile);
            }


            await _context.SaveChangesAsync();
            var contributionOwner = await _userManager.FindByIdAsync(contributionEntity.Email);

            if (user != null && contributionOwner != null)
            {
                var subject = "Thông báo: Tải lên file mới";
                var message = $"Xin chào {contributionOwner.UserName},\n\n" +
                              $"Học sinh đã tải lên file mới cho đóng góp của bạn. Vui lòng kiểm tra và đánh giá.\n\n" +
                              $"Trân trọng,\nUniversity Magazine";

                await _emailService.SendEmailAsync(contributionOwner.Email, subject, message);
            }
            return RedirectToAction("Details", "Student", new { id = contributionEntity.ContributionID });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Có lỗi xảy ra khi tải lên file: {ex.Message}";
            return RedirectToAction("Upload", "Student", new { id = id });
        }
    }




    public async Task<IActionResult> DownloadFiles(string id)
    {
        var files = await _context.Files.Where(f => f.ContributionID == id).ToListAsync();
        if (files == null || files.Count == 0)
        {
            return NotFound();
        }

        // Tạo danh sách các đường dẫn của các file
        var filePaths = files.Select(f => f.FilePath).ToList();

        // Tạo tên file zip
        var zipFileName = $"contribution_{id}_files.zip";

        // Tạo đường dẫn tạm thời cho file zip
        var tempZipFilePath = Path.GetTempFileName();

        using (var zipArchive = new ZipArchive(new FileStream(tempZipFilePath, FileMode.Create), ZipArchiveMode.Create))
        {
            foreach (var filePath in filePaths)
            {
                var entryName = Path.GetFileName(filePath);
                var entry = zipArchive.CreateEntry(entryName);

                using (var entryStream = entry.Open())
                using (var fileStream = new FileStream(Path.Combine("wwwroot", filePath.TrimStart('/')), FileMode.Open))
                {
                    await fileStream.CopyToAsync(entryStream);
                }
            }
        }

        // Đọc file zip tạm thời
        var fileBytes = await System.IO.File.ReadAllBytesAsync(tempZipFilePath);

        // Xóa file zip tạm thời
        System.IO.File.Delete(tempZipFilePath);

        // Trả về file zip để tải về
        return File(fileBytes, "application/zip", zipFileName);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contribution = await _context.Contributions
            .Include(c => c.ApplicationUser) // Đảm bảo rằng thông tin về người dùng cũng được tải lên
            .Include(c => c.Files)
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(c => c.ContributionID == id);

        if (contribution == null)
        {
            return NotFound();
        }

        // Lấy thông tin người dùng hiện tại
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return NotFound();
        }

        // Lấy files liên quan đến contribution cho người dùng hiện tại
        contribution.Files = await _context.Files
            .Where(f => f.ContributionID == contribution.ContributionID && f.StudentEmail == currentUser.Email)
            .ToListAsync();

        return View(contribution);
    }

    // Phương thức để tải xuống một file
    public async Task<IActionResult> DownloadFile(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var file = await _context.Files.FindAsync(id);
        if (file == null)
        {
            return NotFound();
        }

        // Đường dẫn đến file trong wwwroot
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FilePath.TrimStart('/'));

        // Đọc nội dung của file
        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

        return File(fileBytes, "application/octet-stream", file.FileName);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFile(string fileId)
    {
        // Tìm file bằng ID
        var file = await _context.Files.FindAsync(fileId);
        if (file == null)
        {
            return NotFound();
        }

        // Kiểm tra xem có bình luận liên quan không
        var commentsRelatedToFile = await _context.Comments.AnyAsync(c => c.FileID == fileId);
        if (commentsRelatedToFile)
        {
            TempData["ErrorMessage"] = "Không thể xóa tệp tin này vì có bình luận liên quan.";
            return RedirectToAction("Details", new { id = file.ContributionID }); // Chuyển hướng về trang chi tiết đóng góp
        }

        // Kiểm tra xem file có phải là public không
        if (file.IsPublic)
        {
            TempData["ErrorMessage"] = "Không thể xóa tệp tin public.";
            return RedirectToAction("Details", new { id = file.ContributionID }); // Chuyển hướng về trang chi tiết đóng góp
        }

        // Kiểm tra xem tài khoản đăng nhập có phải là tài khoản đã upload file không
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null || file.StudentEmail != currentUser.Email)
        {
            TempData["ErrorMessage"] = "Bạn không có quyền xóa tệp tin này.";
            return RedirectToAction("Details", new { id = file.ContributionID }); // Chuyển hướng về trang chi tiết đóng góp
        }

        // Xóa file
        _context.Files.Remove(file);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Student", new { id = file.ContributionID });
    }
}
