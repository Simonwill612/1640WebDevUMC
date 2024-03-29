using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;

namespace _1640WebDevUMC.Controllers
{
    public class ContributionItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContributionItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContributionItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ContributionItems.Include(c => c.Contribution).Include(c => c.File).Include(c => c.Image);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ContributionItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contributionItem = await _context.ContributionItems
                .Include(c => c.Contribution)
                .Include(c => c.File)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.ContributionItemID == id);
            if (contributionItem == null)
            {
                return NotFound();
            }

            return View(contributionItem);
        }

        // GET: ContributionItems/Create
        public IActionResult Create()
        {
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID");
            ViewData["FileName"] = new SelectList(_context.Files, "FileID", "FileID");
            ViewData["ImageName"] = new SelectList(_context.Images, "ImageID", "ImageID");
            return View();
        }

        // POST: ContributionItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: ContributionItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContributionItemID,ContributionID,Title,Description,UploadDate,FileUpload,ImageUpload")] ContributionItem contributionItem)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem người dùng đã chọn file tải lên chưa
                if (contributionItem.FileUpload != null && contributionItem.ImageUpload != null)
                {
                    // Lưu tên file
                    var fileName = Path.GetFileName(contributionItem.FileUpload.FileName);
                    var imageName = Path.GetFileName(contributionItem.ImageUpload.FileName);

                    // Lưu file vào thư mục tạm
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await contributionItem.FileUpload.CopyToAsync(fileStream);
                    }

                    // Lưu hình ảnh vào thư mục tạm
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", imageName);
                    using (var imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await contributionItem.ImageUpload.CopyToAsync(imageStream);
                    }

                    // Lưu thông tin về tệp và hình ảnh vào đối tượng ContributionItem
                    contributionItem.FileName = fileName;
                    contributionItem.ImageName = imageName;

                    // Thêm ContributionItem vào context và lưu vào cơ sở dữ liệu
                    _context.Add(contributionItem);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Nếu người dùng không chọn file tải lên, hiển thị thông báo lỗi
                    ModelState.AddModelError("", "Please select both file and image to upload.");
                }
            }
            // Nếu ModelState không hợp lệ, hiển thị form tạo mới lại
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID", contributionItem.ContributionID);
            return View(contributionItem);
        }


        // GET: ContributionItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contributionItem = await _context.ContributionItems.FindAsync(id);
            if (contributionItem == null)
            {
                return NotFound();
            }
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID", contributionItem.ContributionID);
            ViewData["FileName"] = new SelectList(_context.Files, "FileID", "FileID", contributionItem.FileName);
            ViewData["ImageName"] = new SelectList(_context.Images, "ImageID", "ImageID", contributionItem.ImageName);
            return View(contributionItem);
        }

        // POST: ContributionItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ContributionItemID,ContributionID,Title,Description,UploadDate,FileName,ImageName")] ContributionItem contributionItem)
        {
            if (id != contributionItem.ContributionItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contributionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContributionItemExists(contributionItem.ContributionItemID))
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
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID", contributionItem.ContributionID);
            ViewData["FileName"] = new SelectList(_context.Files, "FileID", "FileID", contributionItem.FileName);
            ViewData["ImageName"] = new SelectList(_context.Images, "ImageID", "ImageID", contributionItem.ImageName);
            return View(contributionItem);
        }

        // GET: ContributionItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contributionItem = await _context.ContributionItems
                .Include(c => c.Contribution)
                .Include(c => c.File)
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.ContributionItemID == id);
            if (contributionItem == null)
            {
                return NotFound();
            }

            return View(contributionItem);
        }

        // POST: ContributionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contributionItem = await _context.ContributionItems.FindAsync(id);
            if (contributionItem != null)
            {
                _context.ContributionItems.Remove(contributionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContributionItemExists(string id)
        {
            return _context.ContributionItems.Any(e => e.ContributionItemID == id);
        }
    }
}
