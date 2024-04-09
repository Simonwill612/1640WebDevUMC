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
                .Include(c => c.Comments) // Include comments

                .ToListAsync();
            return View(contributions);
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
            return View();
        }

        // POST: Contributions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContributionID,Title,Content,Email,AcademicYearID")] Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", contribution.AcademicYearID);
            ViewData["Email"] = new SelectList(_context.Users, "Id", "Email", contribution.Email);
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
            return View(contribution);
        }

        // POST: Contributions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ContributionID,Title,Content,Email,AcademicYearID")] Contribution contribution)
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





        public IActionResult DownloadFile(string id)
        {
            var contribution = _context.Contributions.Include(c => c.Files).FirstOrDefault(c => c.ContributionID == id);
            if (contribution != null && contribution.Files.Any())
            {
                // Create a list of file paths
                List<string> filePaths = new List<string>();
                foreach (var file in contribution.Files)
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, file.FilePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        filePaths.Add(filePath);
                    }
                }

                if (filePaths.Count > 0)
                {
                    // Create the path for the zip file
                    var zipFileName = $"{contribution.ContributionID}_files.zip";
                    var zipFilePath = Path.Combine(_hostingEnvironment.WebRootPath, zipFileName);

                    // Create a zip file
                    using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                    {
                        foreach (var filePath in filePaths)
                        {
                            zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                        }
                    }

                    // Read data from the zip file and return it to the user
                    var fileBytes = System.IO.File.ReadAllBytes(zipFilePath);

                    // Delete the zip file after it has been created
                    System.IO.File.Delete(zipFilePath);

                    return File(fileBytes, "application/zip", zipFileName);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string contributionId, string fileId, string content)
        {
            // Tìm contribution bằng ID
            var contribution = await _context.Contributions.FindAsync(contributionId);
            if (contribution == null)
            {
                return NotFound();
            }

            // Lấy email của người dùng từ contribution
            var userEmail = contribution.Email;

/*            // Kiểm tra và tạo mới vai trò "Marketing Coordinator" nếu không tồn tại
            var roleExists = await _roleManager.RoleExistsAsync("Marketing Coordinator");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Marketing Coordinator"));
            }

            // Kiểm tra và gán vai trò "Marketing Coordinator" cho người dùng
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null && !(await _userManager.IsInRoleAsync(user, "Marketing Coordinator")))
            {
                await _userManager.AddToRoleAsync(user, "Marketing Coordinator");
            }
*/
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

            // Thêm comment vào database
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Redirect về trang index hoặc trang chi tiết contribution tùy theo logic của bạn
            return RedirectToAction("Index", "Home");
        }

    }
}
