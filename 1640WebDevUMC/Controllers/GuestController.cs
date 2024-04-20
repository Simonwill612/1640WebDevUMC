using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1640WebDevUMC.Controllers
{
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
    }
}
