﻿using System;
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
    public class AcademicYearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcademicYearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcademicYears
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AcademicYears.Include(a => a.Faculty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcademicYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears
                .Include(a => a.Faculty)
                .FirstOrDefaultAsync(m => m.YearID == id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        // GET: AcademicYears/Create
        public IActionResult Create()
        {
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        // POST: AcademicYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YearID,UploadDate,ClosureDate,FinalClosureDate,FacultyID")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", academicYear.FacultyID);
            return View(academicYear);
        }

        // GET: AcademicYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears.FindAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", academicYear.FacultyID);
            return View(academicYear);
        }

        // POST: AcademicYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YearID,UploadDate,ClosureDate,FinalClosureDate,FacultyID")] AcademicYear academicYear)
        {
            if (id != academicYear.YearID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicYearExists(academicYear.YearID))
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
            ViewData["FacultyID"] = new SelectList(_context.Faculties, "FacultyID", "FacultyName", academicYear.FacultyID);
            return View(academicYear);
        }

        // GET: AcademicYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears
                .Include(a => a.Faculty)
                .FirstOrDefaultAsync(m => m.YearID == id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        // POST: AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicYear = await _context.AcademicYears.FindAsync(id);
            if (academicYear != null)
            {
                _context.AcademicYears.Remove(academicYear);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicYearExists(int id)
        {
            return _context.AcademicYears.Any(e => e.YearID == id);
        }
    }
}
