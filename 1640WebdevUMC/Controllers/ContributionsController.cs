using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult ViewUpload()
        {
            var files = _context.Contributions.Where(c => !string.IsNullOrEmpty(c.FilePath)).ToList();
            return View(files);
        }

        public IActionResult DownloadFile(string id)
        {
            var contribution = _context.Contributions.FirstOrDefault(c => c.ContributionID == id);
            if (contribution != null)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, contribution.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
                }
            }
            return NotFound();
        }
    }
}
