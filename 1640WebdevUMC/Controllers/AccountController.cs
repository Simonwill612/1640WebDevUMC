using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;

namespace _1640WebDevUMC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Account(Account account)
        {
            var accounts = await (from user in _context.Users
                                  join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                  join role in _context.Roles on userRole.RoleId equals role.Id
                                  select new Account
                                  {
                                      Id = user.Id,
                                      Email = user.Email,
                                      RoleName = role.Name,
                                  }).ToListAsync();

            return View(accounts);
        }

        public IActionResult Create()
        {
            return View();
        }


        // GET: Administrator/EditAccount/{id}
        public async Task<IActionResult> EditAccount(string id)
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

            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesList = await _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToListAsync();

            var model = new Account
            {
                Id = user.Id,
                Email = user.Email,
                RoleName = userRoles.FirstOrDefault(),
                RolesList = rolesList
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(Account model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                // Update user email
                user.Email = model.Email;

                // Retrieve the role
                var role = await _roleManager.FindByNameAsync(model.RoleName);
                if (role == null)
                {
                    ModelState.AddModelError(string.Empty, "Role not found.");
                    return View(model);
                }

                // Remove user from all roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                var resultRemove = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!resultRemove.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to remove user from roles.");
                    return View(model);
                }

                // Add user to the new role
                var resultAdd = await _userManager.AddToRoleAsync(user, role.Name);
                if (!resultAdd.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to add user to role.");
                    return View(model);
                }

                // Update user
                var resultUpdate = await _userManager.UpdateAsync(user);
                if (resultUpdate.Succeeded)
                {
                    // Redirect with success message
                    TempData["SuccessMessage"] = "User account updated successfully.";
                    return RedirectToAction(nameof(Account));
                }
                else
                {
                    // Add errors to ModelState
                    foreach (var error in resultUpdate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If ModelState is not valid or update fails, return back to the view with the model
            var rolesList = await _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToListAsync();
            model.RolesList = rolesList;
            return View(model);
        }

    }
}
