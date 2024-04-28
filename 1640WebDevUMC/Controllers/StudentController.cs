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
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Student")]
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
            TempData["ErrorMessage"] = "You need to log in to upload files.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            TempData["ErrorMessage"] = "Please confirm your email before uploading the file.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (!await _userManager.IsInRoleAsync(user, "Student"))
        {
            TempData["ErrorMessage"] = "Only students are allowed to upload files.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (files == null || files.Count == 0)
        {
            TempData["ErrorMessage"] = "Please select a file to upload.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        // Declare fileName variable here
        string fileName = "";

        var userFiles = await _context.Files.Where(f => f.StudentEmail == user.Email && f.ContributionID == id).ToListAsync();
        var userUploadedFilesCount = userFiles.Count(f => f.ContributionID == id);

        if (userUploadedFilesCount >= 2)
        {
            TempData["ErrorMessage"] = "You have uploaded a maximum of 2 files for this contribution.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (string.IsNullOrEmpty(id))
        {
            TempData["ErrorMessage"] = "Invalid donation ID.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        var contributionEntity = await _context.Contributions.FindAsync(id);
        if (contributionEntity == null)
        {
            TempData["ErrorMessage"] = "No contributions found.";
            return RedirectToAction("Upload", "Student", new { id = id });
        }

        if (contributionEntity.IsPublic)
        {
            TempData["ErrorMessage"] = "This post is public, you cannot upload files.";
            return RedirectToAction("Details", "Student", new { id = id });
        }

        var existingNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.ContributionID == id);
        if (existingNotification == null)
        {
            var newNotification = new Notification
            {
                ContributionID = id,
                Message = $"Please comment on student files within 14 days.",
                DueDate = DateTime.Now.AddDays(14)
            };

            _context.Notifications.Add(newNotification);
        }

        var academicYear = await _context.AcademicYears.FindAsync(contributionEntity.AcademicYearID);
        if (DateTime.Now > academicYear.FinalClosureDate)
        {
            TempData["ErrorMessage"] = "Expired. You cannot upload files.";
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

                fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}{extension}"; // Assign value to fileName variable here

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
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && contributionOwner != null)
            {
                // Create email content with information about submitter, file name and ContributionID
                var subject = "Notification: Upload new file";
                var message = $"Hello {contributionOwner.UserName},\n\n" +
                              $"Student {currentUser.UserName} has uploaded a new file for your contribution.\n" +
                              $"File name: {fileName}\n" +
                              $"ContributionID: {contributionEntity.ContributionID}\n\n" +
                              $"Please check and rate.\n\n" +
                              $"Best regards,\nUniversity Magazine";

                // Gửi email
                await _emailService.SendEmailAsync(contributionOwner.Email, subject, message);
            }

            return RedirectToAction("Details", "Student", new { id = contributionEntity.ContributionID });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred while uploading the file: {ex.Message}";
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

        // Create a list of paths of files
        var filePaths = files.Select(f => f.FilePath).ToList();

        // Create zip file name
        var zipFileName = $"contribution_{id}_files.zip";

        // Create a temporary path for the zip file
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

        // Read temporary zip file
        var fileBytes = await System.IO.File.ReadAllBytesAsync(tempZipFilePath);

        // Delete temporary zip file
        System.IO.File.Delete(tempZipFilePath);

        // Returns the zip file to download
        return File(fileBytes, "application/zip", zipFileName);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contribution = await _context.Contributions
            .Include(c => c.ApplicationUser)
            .Include(c => c.Files)
                .ThenInclude(f => f.Comments)
                    .ThenInclude(c => c.ApplicationUser)
            .FirstOrDefaultAsync(c => c.ContributionID == id);

        if (contribution == null)
        {
            return NotFound();
        }

        // Get current user information
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return NotFound();
        }

        if (contribution.IsPublic)
        {
            // If the post is public, display all public files and comments of each file
            // Includes both unlisted and public comments
            foreach (var file in contribution.Files)
            {
                if (file.IsPublic)
                {
                    // Get all comments of the file
                    var allComments = file.Comments.ToList();
                    // If the post is public, include both unlisted and public comments of the file
                    file.Comments = allComments.Where(c => c.FileID == file.FileID && (!c.IsPublic || c.IsPublic && contribution.IsPublic)).ToList();
                }
            }
        }
        else
        {
            // If the post is private, only show the current user's unlisted files and comments
            contribution.Files = contribution.Files.Where(f => !f.IsPublic && f.StudentEmail == currentUser.Email).ToList();
            foreach (var file in contribution.Files)
            {
                file.Comments = file.Comments.Where(c => !c.IsPublic && c.FileID == file.FileID && c.Email == currentUser.Email).ToList();
            }
        }

        return View(contribution);
    }


    // Method to download a file
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

        // Path to file in wwwroot
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FilePath.TrimStart('/'));

        // Read the content of the file
        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

        return File(fileBytes, "application/octet-stream", file.FileName);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFile(string fileId)
    {
        // Find files by ID
        var file = await _context.Files.FindAsync(fileId);
        if (file == null)
        {
            return NotFound();
        }

        // Check if there are related comments
        var commentsRelatedToFile = await _context.Comments.AnyAsync(c => c.FileID == fileId);
        if (commentsRelatedToFile)
        {
            TempData["ErrorMessage"] = "Không thể xóa tệp tin này vì có bình luận liên quan.";
            return RedirectToAction("Details", new { id = file.ContributionID }); // Redirect to the contribution details page
        }

        // Check if the file is public
        if (file.IsPublic)
        {
            TempData["ErrorMessage"] = "Không thể xóa tệp tin public.";
            return RedirectToAction("Details", new { id = file.ContributionID }); // Redirect to the contribution details page
        }

        // Check if the login account is the account that uploaded the file
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null || file.StudentEmail != currentUser.Email)
        {
            TempData["ErrorMessage"] = "Bạn không có quyền xóa tệp tin này.";
            return RedirectToAction("Details", new { id = file.ContributionID }); // Redirect to the contribution details page
        }

        // Delete file
        _context.Files.Remove(file);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Student", new { id = file.ContributionID });
    }

    [HttpPost]
    public async Task<IActionResult> AddOtherComment(string contributionId, string content, string fileId)
    {
        if (string.IsNullOrEmpty(contributionId) || string.IsNullOrEmpty(content))
        {
            TempData["ErrorMessage"] = "Contribution ID or comment content is missing.";
            return RedirectToAction("Details", new { id = contributionId });
        }

        if (string.IsNullOrEmpty(fileId))
        {
            TempData["ErrorMessage"] = "File ID is required for adding a comment.";
            return RedirectToAction("Details", new { id = contributionId });
        }

        var contribution = await _context.Contributions.FindAsync(contributionId);
        if (contribution == null)
        {
            return NotFound();
        }

        if (!User.Identity.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "You must be logged in to add a comment.";
            return RedirectToAction("Details", new { id = contributionId });
        }

        var currentUser = await _userManager.GetUserAsync(User);

        var file = await _context.Files.FindAsync(fileId);
        if (file == null)
        {
            TempData["ErrorMessage"] = "File not found.";
            return RedirectToAction("Details", new { id = contributionId });
        }

        // Check if the file must be undeclared
        if (!contribution.IsPublic)
        {
            TempData["ErrorMessage"] = "You cannot add comments to a non-public contribution.";
            return RedirectToAction("Details", new { id = contributionId });
        }

        var comment = new Comment
        {
            CommentID = $"{contributionId}_{Guid.NewGuid().ToString()}",
            Content = content,
            CommentDate = DateTime.Now,
            FileID = fileId,
            Email = currentUser.Id,
            ContributionID = contributionId,
            IsPublic = true // Set IsPublic to true for non-public comments
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = contributionId });
    }







    public async Task<IActionResult> DeleteComment(string commentId)
    {
        // Find the comment by its ID
        var commentToDelete = _context.Comments.Find(commentId);

        if (commentToDelete == null)
        {
            // Comment not found, return NotFound() or handle the error as appropriate
            return NotFound();
        }

        // Check if the user is authorized to delete the comment
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            // User is not logged in, return Unauthorized() or handle the error as appropriate
            return Unauthorized();
        }

        // Check if the current user is the owner of the comment
        if (commentToDelete.Email != currentUser.Id)
        {
            // Current user is not the owner of the comment, return Forbidden() or handle the error as appropriate
            return Forbid();
        }

        // If the user is authorized, proceed with deleting the comment
        _context.Comments.Remove(commentToDelete);
        await _context.SaveChangesAsync(); // <-- Changed to await SaveChangesAsync

        // Redirect to the same page or wherever appropriate after deleting the comment
        return RedirectToAction("Details", new { id = commentToDelete.ContributionID });
    }





}