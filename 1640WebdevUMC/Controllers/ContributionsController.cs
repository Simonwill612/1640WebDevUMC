using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Models;
using _1640WebDevUMC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ContributionsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ContributionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Contributions
    public async Task<IActionResult> Index()
    {
        return View(await _context.Contributions.ToListAsync());
    }

    // GET: Contributions/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(m => m.ContributionID == id);
        if (contribution == null)
        {
            return NotFound();
        }

        return View(contribution);
    }

    // GET: Contributions/Create
    public async Task<IActionResult> Create()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null && await _userManager.IsInRoleAsync(user, "Student"))
        {
            ViewBag.Email = user.Email;
            ViewBag.AcademicYearID = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID");
            return View();
        }
        else
        {
            return View("Error", new ErrorViewModel { RequestId = "User must be a student to make a contribution." });
        }
    }

    // POST: Contributions/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ContributionID,File,Image,AcademicYearID,Email")] Contribution contribution)
    {
        var user = await _userManager.FindByEmailAsync(contribution.Email);
        if (ModelState.IsValid)
        {
            var academicYear = _context.AcademicYears.Find(contribution.AcademicYearID);
            if (academicYear != null && user != null)
            {
                _context.Add(contribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "The academic year is not valid or the user does not exist. You cannot submit your assignment.");
            }
        }
        // Repopulate ViewBag values
        ViewBag.Email = user?.Email;
        ViewBag.AcademicYearID = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", contribution.AcademicYearID);
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
        return View(contribution);
    }

    // POST: Contributions/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("ContributionID,File,Image,AcademicYearID,Email")] Contribution contribution)
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
        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ContributionExists(string id)
    {
        return _context.Contributions.Any(e => e.ContributionID == id);
    }
}
