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
        public IActionResult Submit()
        {
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID");
            ViewData["FileID"] = new SelectList(_context.Files, "FileID", "FileName");
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageName");
            return View();
        }

        // POST: ContributionItems/Submit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([Bind("ContributionItemID,ContributionID,UploadDate,FileID,ImageID")] ContributionItem contributionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contributionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID", contributionItem.ContributionID);
            ViewData["FileID"] = new SelectList(_context.Files, "FileID", "FileName", contributionItem.FileID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageName", contributionItem.ImageID);
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
            ViewData["FileID"] = new SelectList(_context.Files, "FileID", "FileName", contributionItem.FileID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageName", contributionItem.ImageID);
            return View(contributionItem);
        }

        // POST: ContributionItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ContributionItemID,ContributionID,UploadDate,FileID,ImageID")] ContributionItem contributionItem)
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
            ViewData["FileID"] = new SelectList(_context.Files, "FileID", "FileName", contributionItem.FileID);
            ViewData["ImageID"] = new SelectList(_context.Images, "ImageID", "ImageName", contributionItem.ImageID);
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
    

public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, IFormFile image, ContributionItem contributionItem)
        {
            // Check if the file is a PDF
            if (file != null)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".pdf")
                {
                    var fileModel = new Models.File
                    {
                        FileID = file.FileName,
                        FileName = file.FileName, // Set the FileName property instead of FileID
                        FileData = await GetBytesFromFile(file)
                    };

                    _context.Files.Add(fileModel);
                    await _context.SaveChangesAsync();

                    // Update the ContributionItem with the new FileID
                    contributionItem.FileID = fileModel.FileID;
                }
                else
                {
                    ModelState.AddModelError("", "File must be a PDF.");
                    return View();
                }
            }

            // Handle the image upload
            if (image != null)
            {
                var imageModel = new Models.Image
                {
                    ImageID = image.FileName,
                    ImageName = image.FileName, // Set the ImageName property instead of ImageID
                    ImageData = await GetBytesFromFile(image) // Assuming GetBytesFromFile can be used for images as well
                };

                _context.Images.Add(imageModel);
                await _context.SaveChangesAsync();

                // Update the ContributionItem with the new ImageID
                contributionItem.ImageID = imageModel.ImageID;
            }

            // Save the updated ContributionItem
            _context.ContributionItems.Update(contributionItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<byte[]> GetBytesFromFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}
