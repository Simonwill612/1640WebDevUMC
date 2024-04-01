using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using _1640WebDevUMC.Data;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
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
public IActionResult Upload(string id, List<IFormFile> files)
{
    try
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("Please select at least one file to upload.");
        }

        var contribution = _context.Contributions.Find(id);
        if (contribution == null)
        {
            return NotFound();
        }

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (extension != ".pdf" && extension != ".png")
                {
                    return BadRequest("Invalid file type. Only PDF and PNG files are allowed.");
                }

                var folder = extension == ".pdf" ? "files" : "images";
                var directoryPath = Path.Combine("wwwroot", folder, id);

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

                // Assuming there's a property FilePath in Contribution
                contribution.FilePath = $"/{folder}/{id}/{fileName}";

                // Log file upload information
                Console.WriteLine($"File uploaded successfully: {fileName}, ContributionID: {contribution.ContributionID}");
            }
        }

        _context.SaveChanges();

            return RedirectToAction("Index", "Student");
        }
        catch (Exception ex)
    {
        // Log the exception
        Console.WriteLine($"An error occurred while uploading files: {ex.Message}");
        return StatusCode(500, $"An error occurred while uploading files: {ex.Message}");
    }
}

}
