using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _1640WebDevUMC.Controllers
{
    [Authorize(Roles = "Guest")]
    public class GuestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public GuestController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
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

        // GET: /Account/Edit
        // GET: /Account/Edit
        // GET: /Account/Edit


        public async Task<IActionResult> UpdateContributions(string id, int newContributions)
        {
            // Find the user
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update NumberOfContributions
            user.NumberOfContributions = newContributions;

            // Save changes
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Handle errors...
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FilePath.TrimStart('/'));
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileBytes, "application/octet-stream", file.FileName);
        }
    }
}
