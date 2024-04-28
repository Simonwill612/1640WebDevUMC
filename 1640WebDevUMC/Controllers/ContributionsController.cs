using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using System.IO.Compression;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _1640WebDevUMC.Controllers
{
    [Authorize(Roles = "Marketing Coordinator,Marketing Manager")]

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
        public async Task<IActionResult> GetFileCountByUpdateTime()
        {
            var fileCountByUpdateTime = await _context.Files
                .GroupBy(f => f.UploadTime.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();
            return Json(fileCountByUpdateTime);
        }
        public async Task<IActionResult> GetCommentCount()
        {
            var commentCount = await _context.Comments.CountAsync();
            return Json(commentCount);
        }

        public async Task<IActionResult> GetUserCount()
        {
            var userCount = await _context.Users.CountAsync();
            return Json(userCount);
        }

        public async Task<IActionResult> GetIsPublicSummary()
        {
            var isPublicSummary = await _context.Contributions
                .GroupBy(c => c.IsPublic)
                .Select(g => new { IsPublic = g.Key, Count = g.Count() })
                .ToListAsync();

            return Json(isPublicSummary);
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
        public async Task<IActionResult> SetPublic(string fileId, bool makePublic, string contributionId)
        {
            if (fileId == null || contributionId == null)
            {
                // Handle when data is invalid
                return RedirectToAction("Details", "Contributions", new { id = contributionId });
            }

            var file = await _context.Files.FindAsync(fileId);
            if (file != null)
            {
                // Update the public/private status of the file
                file.IsPublic = makePublic;

                // Update public/private status of comments if necessary
                if (file.Comments != null)
                {
                    foreach (var comment in file.Comments)
                    {
                        comment.IsPublic = makePublic;
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
            }

            // Redirect to the contribution detail page
            return RedirectToAction("Details", "Contributions", new { id = contributionId });
        }




        // GET: Contributions/Details/5
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
                .Include(c => c.Comments)
                .Include(c => c.Files)
                .FirstOrDefaultAsync(m => m.ContributionID == id);

            if (contribution == null)
            {
                return NotFound();
            }

            // Check to see if there are any comments without email or "N/A" email
            var hasNonNAComments = contribution.Comments.Any(c => !string.IsNullOrEmpty(c.Email) && c.Email != "N/A");

            // Only pass this variable to the view if there is at least one comment without an email or the email is not "N/A"
            ViewData["HasNonNAComments"] = hasNonNAComments;

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

            // Delete all files related to the article
            _context.Files.RemoveRange(contribution.Files);

            // Delete all comments related to the article
            foreach (var file in contribution.Files)
            {
                _context.Comments.RemoveRange(file.Comments);
            }

            // Delete posts from database
            _context.Contributions.Remove(contribution);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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

            // Path to file in wwwroot
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FilePath.TrimStart('/'));

            // Read the content of the file
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

        [HttpPost]
        public async Task<IActionResult> AddComment(string contributionId, string fileId, string content)
        {
            if (string.IsNullOrEmpty(fileId) || string.IsNullOrEmpty(contributionId))
            {
                return BadRequest("File ID and Contribution ID are required for adding a comment.");
            }

            var contribution = await _context.Contributions.FindAsync(contributionId);
            if (contribution == null)
            {
                return NotFound();
            }

            var file = await _context.Files.FindAsync(fileId);
            if (file == null || file.ContributionID != contributionId)
            {
                return BadRequest("Invalid file ID or contribution ID.");
            }

            var comment = new Comment
            {
                CommentID = $"{contributionId}_{Guid.NewGuid().ToString()}",
                Content = content,
                CommentDate = DateTime.Now,
                FileID = fileId,
                Email = contribution.Email, // Use contribution email
                ContributionID = contributionId,
                IsPublic = file.IsPublic
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

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