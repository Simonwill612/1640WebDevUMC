using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;

namespace _1640WebDevUMC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var accounts = await _userManager.Users.ToListAsync();
            var model = new List<Accounts>();

            foreach (var user in accounts)
            {
                var role = await _userManager.GetRolesAsync(user);
                var account = new Accounts
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleName = role.FirstOrDefault()
                };
                model.Add(account);
            }

            return View(model);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(user);
            var account = new Accounts
            {
                Id = user.Id,
                Email = user.Email,
                RoleName = role.FirstOrDefault()
            };

            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var rolesList = await GetRolesList();

            var model = new Accounts
            {
                Id = user.Id,
                Email = user.Email,
                RoleName = roles.FirstOrDefault(),
                RolesList = rolesList
            };

            return View(model);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Accounts model)
        {
            if (id != model.Id || !ModelState.IsValid)
            {
                model.RolesList = await GetRolesList();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.Email = model.Email;

            // Remove current roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Assign new role if specified
            if (!string.IsNullOrEmpty(model.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(model.RoleName);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            // Attempt to update user
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }



        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(user);
            var account = new Accounts
            {
                Id = user.Id,
                Email = user.Email,
                RoleName = role.FirstOrDefault()
            };

            return View(Index);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetRolesList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
        }
    }
}
