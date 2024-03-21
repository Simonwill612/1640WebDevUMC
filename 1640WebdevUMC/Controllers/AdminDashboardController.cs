using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AdminDashboardController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public AdminDashboardController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
    }

    // GET: /Account/Index
    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var userData = new List<UserViewModel>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userData.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                Roles = roles
            });
        }
        return View(userData);
    }

    // GET: /Account/Details
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userDetails = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PasswordHash = user.PasswordHash,
            Roles = await _userManager.GetRolesAsync(user)
        };

        return View(userDetails);
    }

    // GET: /Account/Edit
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList(); // Get all roles

        var model = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PasswordHash = user.PasswordHash,
            Roles = userRoles,
            AllRoles = allRoles // Set all roles
        };

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> EditUser(UserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null)
        {
            return NotFound();
        }

        // Update the user properties
        user.Email = model.Email;
        user.EmailConfirmed = model.EmailConfirmed;

        // Update the user password
        if (!string.IsNullOrEmpty(model.PasswordHash))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.PasswordHash);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }
        }

        // Get the current user roles
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Determine roles to be added (roles in the model that are not in the current roles)
        var rolesToAdd = model.Roles.Except(currentRoles);

        // Determine roles to be removed (roles in the current roles that are not in the model)
        var rolesToRemove = currentRoles.Except(model.Roles);

        // Add new roles
        foreach (var role in rolesToAdd)
        {
            var addResult = await _userManager.AddToRoleAsync(user, role);
            if (!addResult.Succeeded)
            {
                AddErrors(addResult);
                return View(model);
            }
        }

        // Remove roles not in the model
        foreach (var role in rolesToRemove)
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(user, role);
            if (!removeResult.Succeeded)
            {
                AddErrors(removeResult);
                return View(model);
            }
        }

        // Save changes
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            AddErrors(updateResult);
            return View(model);
        }

        return RedirectToAction("Index");
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }




    // POST: /Account/Delete
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid user ID");
        }

        if (_userManager == null)
        {
            return NotFound();
        }

        var user = await _userManager?.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var currentUser = await _userManager?.GetUserAsync(User);
        if (currentUser == null)
        {
            return BadRequest("No current user found");
        }

        if (currentUser.Id == user.Id)
        {
            return BadRequest("Cannot delete the currently logged in admin account");
        }

        var result = await _userManager?.DeleteAsync(user);
        if (result == null || !result.Succeeded)
        {
            return BadRequest("Failed to delete user");
        }

        return RedirectToAction(nameof(Index));
    }

}
