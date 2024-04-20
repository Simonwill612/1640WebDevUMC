using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using System.IO.Compression;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace _1640WebDevUMC.Controllers
{
    public class ContributionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContributionsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // GET: Contributions
        public async Task<IActionResult> Index()
        {
            var contributions = await _context.Contributions
                .Include(c => c.AcademicYear)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Comments)
                .ToListAsync();
            return View(contributions);
        }

        // Other actions...

        // Helper method to check if a contribution is public
        private bool IsContributionPublic(string contributionId)
        {
            // Check if the contribution is associated with a public academic year
            var contribution = _context.Contributions
                .Include(c => c.AcademicYear)
                .FirstOrDefault(c => c.ContributionID == contributionId);

            if (contribution != null && contribution.AcademicYear != null && contribution.IsPublic)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<IActionResult> SetPublic(List<string> fileIds)
        {
            if (fileIds == null || fileIds.Count == 0)
            {
                // Handle case where no files are selected
                return RedirectToAction("Details", "Contributions"); // Redirect to appropriate action
            }

            foreach (var fileId in fileIds)
            {
                var file = await _context.Files.FindAsync(fileId);
                if (file != null)
                {
                    // Set the file as public
                    file.IsPublic = true;

                    // Set comments as public as well if needed
                    if (file.Comments != null)
                    {
                        foreach (var comment in file.Comments)
                        {
                            comment.IsPublic = true;
                        }
                    }
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect back to the index page or any other appropriate page
            return RedirectToAction("Index", "Contributions");
        }

        // GET: Contributions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _context.Contributions
                .Include(c => c.AcademicYear)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Comments) // Include comments
                .Include(c => c.Files) // Include files
                .FirstOrDefaultAsync(m => m.ContributionID == id);

            if (contribution == null)
            {
                return NotFound();
            }

            return View(contribution);
        }

        // GET: Contributions/Create
        public IActionResult Create()
        {
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID");
            ViewData["Email"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["IsPublicOptions"] = new SelectList(new[] { new { Value = true, Text = "Yes" }, new { Value = false, Text = "No" } }, "Value", "Text");
            return View();
        }

        // POST: Contributions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContributionID,Title,Content,Email,AcademicYearID,IsPublic")] Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", contribution.AcademicYearID);
            ViewData["Email"] = new SelectList(_context.Users, "Id", "Email", contribution.Email);
            ViewData["IsPublicOptions"] = new SelectList(new[] { new { Value = true, Text = "Yes" }, new { Value = false, Text = "No" } }, "Value", "Text", contribution.IsPublic);
            return View(contribution);
        }


        // GET: Contributions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _context.Contributions.FindAsync(id);
            if (contribution == null)
            {
                return NotFound();
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", contribution.AcademicYearID);
            ViewData["Email"] = new SelectList(_context.Users, "Id", "Email", contribution.Email);
            ViewData["IsPublicOptions"] = new SelectList(new[] { new { Value = true, Text = "Yes" }, new { Value = false, Text = "No" } }, "Value", "Text", contribution.IsPublic);
            return View(contribution);
        }

        // POST: Contributions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ContributionID,Title,Content,Email,AcademicYearID,IsPublic")] Contribution contribution)
        {
            if (id != contribution.ContributionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contribution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContributionExists(contribution.ContributionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", contribution.AcademicYearID);
            ViewData["Email"] = new SelectList(_context.Users, "Id", "Email", contribution.Email);
            ViewData["IsPublicOptions"] = new SelectList(new[] { new { Value = true, Text = "Yes" }, new { Value = false, Text = "No" } }, "Value", "Text", contribution.IsPublic);
            return View(contribution);
        }


        // GET: Contributions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _context.Contributions
                .Include(c => c.AcademicYear)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ContributionID == id);
            if (contribution == null)
            {
                return NotFound();
            }

            return View(contribution);
        }

        // POST: Contributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contribution = await _context.Contributions.FindAsync(id);
            if (contribution != null)
            {
                _context.Contributions.Remove(contribution);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContributionExists(string id)
        {
            return _context.Contributions.Any(e => e.ContributionID == id);
        }





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


        [HttpPost]
        public async Task<IActionResult> AddComment(string contributionId, string fileId, string content)
        {
            // Check if fileId is provided
            if (string.IsNullOrEmpty(fileId))
            {
                // Handle the case where fileId is not provided
                return BadRequest("File ID is required for adding a comment.");
            }

            // Tìm contribution bằng ID
            var contribution = await _context.Contributions.FindAsync(contributionId);
            if (contribution == null)
            {
                return NotFound();
            }

            // Lấy email của người dùng từ contribution
            var userEmail = contribution.Email;

            // Tạo mới một comment
            var comment = new Comment
            {
                CommentID = $"{contributionId}_{Guid.NewGuid().ToString()}",
                Content = content,
                CommentDate = DateTime.Now,
                FileID = fileId,
                Email = userEmail,
                ContributionID = contributionId
            };
            ViewData["Email"] = new SelectList(_context.Users, "Id", "Email");

            // Thêm comment vào database
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Redirect về trang index hoặc trang chi tiết contribution tùy theo logic của bạn
            return RedirectToAction("Details", "Contributions", new { id = contributionId });
        }

        // POST: Contributions/DeleteComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            // Find the comment by ID
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                // If comment not found, return Not Found status
                return NotFound();
            }

            // Remove the comment from the context
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            // Redirect back to the details page of the contribution
            return RedirectToAction("Details", new { id = comment.ContributionID });
        }
    }
}
