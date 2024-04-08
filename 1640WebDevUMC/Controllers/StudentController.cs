using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using _1640WebDevUMC.Data;
using Microsoft.AspNetCore.Identity;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                                                .Include(c => c.Comments) // Include comments

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
            ModelState.AddModelError(string.Empty, "You must be logged in to upload files.");
            return View();
        }

        // Check if the user's email is confirmed
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            ModelState.AddModelError(string.Empty, "Please confirm your email before uploading files.");
            return View();
        }

        // Check if the user has the "Student" role
        if (!await _userManager.IsInRoleAsync(user, "Student"))
        {
            ModelState.AddModelError(string.Empty, "Only students are allowed to upload files.");
            return View();
        }

        // Check if a file is selected
        if (files == null || files.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Please select a file to upload.");
            return View();
        }

        var contribution = _context.Contributions.Find(id);
        if (contribution == null)
        {
            ModelState.AddModelError(string.Empty, "Contribution not found.");
            return View();
        }

        // Fetch the AcademicYear using the AcademicYearID from the contribution
        var academicYear = _context.AcademicYears.Find(contribution.AcademicYearID);

        // Check if the deadline has passed
        if (DateTime.Now > academicYear.FinalClosureDate)
        {
            ModelState.AddModelError(string.Empty, "The deadline has passed. You cannot upload files.");
            return View();
        }

        try
        {
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (extension != ".pdf" && extension != ".png")
                {
                    ModelState.AddModelError(string.Empty, "Invalid file type. Only PDF and PNG files are allowed.");
                    return View();
                }

                // Determine the folder based on the file type
                var folder = extension == ".pdf" ? "files" : "images";
                var directoryPath = Path.Combine("wwwroot", folder, contribution.ContributionID);

                // Check if the directory exists, create a new directory if it doesn't
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}{extension}";
                var filePath = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Use a relative path for the file
                var relativeFilePath = $"/{folder}/{contribution.ContributionID}/{fileName}";

                // Create a new File object
                var uploadedFile = new _1640WebDevUMC.Models.File
                {
                    ContributionID = contribution.ContributionID,
                    FileName = fileName,
                    FilePath = relativeFilePath,
                    StudentEmail = user.Email
                };

                // Add the new File object to the database
                _context.Files.Add(uploadedFile);
            }

            _context.SaveChanges();

            return Ok("Files uploaded successfully.");
        }
        catch (Exception ex)
        {
            // Handle errors during file saving
            ModelState.AddModelError(string.Empty, $"An error occurred while uploading the files: {ex.Message}");
            return View();
        }
    }



    [HttpPost]
public async Task<IActionResult> AddComment(string contributionId, string content)
{
    // Tìm contribution bằng ID
    var contribution = _context.Contributions.Find(contributionId);
    if (contribution == null)
    {
        return NotFound();
    }
        ViewData["Email"] = new SelectList(_context.Comments.Select(c => c.Email), "Email", "Email");

        // Sử dụng email từ contribution
        var email = contribution.Email;

    // Tạo CommentID dựa trên ContributionID
    var commentId = contributionId + "_" + Guid.NewGuid().ToString();

    var comment = new Comment
    {
        CommentID = commentId,
        ContributionID = contributionId,
        Email = email,
        Content = content,
        CommentDate = DateTime.Now
    };

    _context.Comments.Add(comment);
    await _context.SaveChangesAsync();
        ViewData["Email"] = new SelectList(_context.Comments.Select(c => c.Email), "Email", "Email");

        return RedirectToAction("Index");
}
    [HttpPost]
    public async Task<IActionResult> DeleteComment(string commentId)
    {
        // Tìm comment bằng ID
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment == null)
        {
            return NotFound();
        }

        // Xóa comment
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        ViewData["Email"] = new SelectList(_context.Comments.Select(c => c.Email), "Email", "Email");

        return RedirectToAction("Index");
    }


}
