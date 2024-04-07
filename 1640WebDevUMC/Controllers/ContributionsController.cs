using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using System.IO.Compression;
using System.Net;

namespace _1640WebDevUMC.Controllers
{
    public class ContributionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ContributionsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Contributions
        public async Task<IActionResult> Index()
        {
            var contributions = await _context.Contributions
                .Include(c => c.AcademicYear)
                .Include(c => c.ApplicationUser)
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
            var contribution = _context.Contributions.FirstOrDefault(c => c.ContributionID == id);
            if (contribution != null && contribution.FilePath != null)
            {
                foreach (var path in contribution.FilePath)
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, path.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        // Create the path for the zip file
                        var zipFilePath = Path.ChangeExtension(filePath, ".zip");

                        // Create a zip file and add the original file to it
                        ZipFile.CreateFromDirectory(Path.GetDirectoryName(filePath), zipFilePath, CompressionLevel.Fastest, false);

                        // Read data from the zip file and return it to the user
                        var fileBytes = System.IO.File.ReadAllBytes(zipFilePath);
                        var zipFileName = Path.ChangeExtension(Path.GetFileName(filePath), ".zip");

                        // Delete the zip file after it has been created
                        System.IO.File.Delete(zipFilePath);

                        return File(fileBytes, "application/zip", zipFileName);
                    }
                }
            }
            return NotFound();
        }
       
/*        [HttpPost]
        public async Task<IActionResult> Create(string contributionId, string content)
        {
            var comment = new Comment
            {
                ContributionID = contributionId,
                Content = content,
                // Add other fields as necessary
            };

            _context.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Contributions", new { id = contributionId });
        }*/
    }
}
