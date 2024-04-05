using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using _1640WebDevUMC.Data;
using Microsoft.AspNetCore.Identity;
using _1640WebDevUMC.Models;

public class StudentController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public StudentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var contributions = await _context.Contributions
            .Include(c => c.AcademicYear)
                .ThenInclude(a => a.Faculty) // Include Faculty within AcademicYear
            .Include(c => c.ApplicationUser)
            .ToListAsync();

        return View(contributions);
    }
    public async Task<IActionResult> Upload(string id)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(string id, List<IFormFile> files)
    {
        // Check if the user is logged in
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        // Check if the user's email is confirmed
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return BadRequest("Please confirm your email before uploading files.");
        }

        // Check if the user has the "Student" role
        if (!await _userManager.IsInRoleAsync(user, "Student"))
        {
            return BadRequest("Only students are allowed to upload files.");
        }

        // Check if a file is selected
        if (files == null || files.Count == 0)
        {
            return BadRequest("Please select a file to upload.");
        }

        var contribution = _context.Contributions.Find(id);
        if (contribution == null)
        {
            return NotFound();
        }

        // Fetch the AcademicYear using the AcademicYearID from the contribution
        var academicYear = _context.AcademicYears.Find(contribution.AcademicYearID);

        // Check if the deadline has passed
        if (DateTime.Now > academicYear.FinalClosureDate)
        {
            return BadRequest("The deadline has passed. You cannot upload files.");
        }

        try
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (extension != ".pdf" && extension != ".png")
                {
                    return BadRequest("Invalid file type. Only PDF and PNG files are allowed.");
                }

                // Determine the folder based on the file type
                var folder = extension == ".pdf" ? "files" : "images";
                var directoryPath = Path.Combine("wwwroot", folder, contribution.ContributionID);

                // Check if the directory exists, create a new directory if it doesn't
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Use a relative path for the file
                var relativeFilePath = $"/{folder}/{contribution.ContributionID}/{fileName}";
                contribution.FilePath.Add(relativeFilePath); // Add the new path to the FilePath list
            }

            _context.SaveChanges();

            return Ok("Files uploaded successfully.");
        }
        catch (Exception ex)
        {
            // Handle errors during file saving
            return StatusCode(500, $"An error occurred while uploading the files: {ex.Message}");
        }
    }




}
