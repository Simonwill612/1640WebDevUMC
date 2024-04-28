using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1640WebDevUMC.Controllers
{
    [Authorize(Roles = "Marketing Managerment")]
    public class MarketingManagementDashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MarketingManagementDashboardController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var contributions = await _context.Contributions
                .Include(c => c.AcademicYear)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Comments)
                .ToListAsync();
            return View(contributions);
        }

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
    }
}
