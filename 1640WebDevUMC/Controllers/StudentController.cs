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
    public IActionResult Upload(string id, IFormFile file)
    {
        // Kiểm tra xem có tệp tin được chọn hay không
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please select a file to upload.");
        }

        var contribution = _context.Contributions.Find(id);
        if (contribution == null)
        {
            return NotFound();
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (extension != ".pdf" && extension != ".png")
        {
            return BadRequest("Invalid file type. Only PDF and PNG files are allowed.");
        }

        try
        {
            var folder = extension == ".pdf" ? "files" : "images";
            var fileName = $"{Guid.NewGuid()}{extension}";
            var directoryPath = Path.Combine("wwwroot", folder);

            // Kiểm tra nếu thư mục không tồn tại, tạo mới thư mục
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Sử dụng đường dẫn tương đối cho tệp tin
            contribution.FilePath = $"/{folder}/{fileName}";
            _context.SaveChanges();

            return Ok("File uploaded successfully.");
        }
        catch (Exception ex)
        {
            // Xử lý lỗi trong quá trình lưu tệp tin
            return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
        }
    }
}
