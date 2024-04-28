using _1640WebDevUMC.Data;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1640WebDevUMC.Controllers
{
    [Authorize(Roles = "Marketing Coordinator")]

    public class MarketingCoordinatorDashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MarketingCoordinatorDashboardController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return BadRequest("No current user found");
            }

            // Get the name of the department the current user belongs to
            var userFacultyName = _context.Faculties
                .Where(f => f.Users.Any(u => u.Id == currentUser.Id))
                .Select(f => f.FacultyName)
                .FirstOrDefault();

            // Get a list of all users belonging to the same department (based on faculty name)
            var usersInSameFaculty = await _userManager.Users
                .Where(u => _context.Faculties.Any(f => f.Users.Any(u2 => u2.Id == u.Id) && f.FacultyName == userFacultyName))
                .ToListAsync();

            var userData = new List<UserViewModel>();
            foreach (var user in usersInSameFaculty)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // Check if the user belongs to the role "Guest"
                if (roles.Contains("Guest"))
                {
                    // Get the list of faculty to which the user belongs
                    var faculties = await _context.Faculties
                        .Where(f => f.Users.Any(u => u.Id == user.Id))
                        .Select(f => f.FacultyName)
                        .ToListAsync();

                    // Add user information to the list
                    userData.Add(new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        PasswordHash = user.PasswordHash,
                        Roles = roles,
                        Faculties = faculties
                    });
                }
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

            var userRoles = await _userManager.GetRolesAsync(user);
            var faculties = _context.Faculties.Where(f => f.Users.Any(u => u.Id == user.Id)).Select(f => f.FacultyName).ToList(); // Get the faculty names

            var userDetails = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PasswordHash = user.PasswordHash,
                Roles = userRoles,
                Faculties = faculties // Set faculties
            };

            return View(userDetails);
        }
        /*        [HttpPost]
                public async Task<IActionResult> SetPublic(string fileId, bool makePublic, string contributionId)
                {
                    if (fileId == null || contributionId == null)
                    {
                        // Handle when data is invalid
                        return RedirectToAction("Details", "Contributions", new { id = contributionId });
                    }

                    var file = await _context.Files.FindAsync(fileId);
                    if (file != null)
                    {
                        // Update the public/private status of the file
                        file.IsPublic = makePublic;

                       // Update public/private status of comments if necessary
                        if (file.Comments != null)
                        {
                            foreach (var comment in file.Comments)
                            {
                                comment.IsPublic = makePublic;
                            }
                        }

                       // Save changes to the database
                        await _context.SaveChangesAsync();
                    }

                    // Redirect to the contribution detail page
                    return RedirectToAction("Details", "Contributions", new { id = contributionId });
                }

                // Helper method to check if a contribution is public
                private bool IsContributionPublic(string contributionId)
                {
                    // Check if the contribution is associated with a public academic year
                    var contribution = _context.Contributions
                        .Include(c => c.AcademicYear)
                        .FirstOrDefault(c => c.ContributionID == contributionId);

                    if (contribution != null && contribution.AcademicYear != null && contribution.IsPublic)
                    {
                        return true;
                    }

                    return false;
                }*/



        // GET: MarkettingManager

        public async Task<IActionResult> Edit(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var userRoles = await _userManager.GetRolesAsync(user);

            // Get a list of all roles
            var allRoles = _roleManager.Roles
                    .Where(role => role.Name == "Guest") // Only take the role "Guest"
                    .Select(r => r.Name)
                    .ToList();

                var faculties = _context.Faculties.Where(f => f.Users.Any(u => u.Id == user.Id)).Select(f => f.FacultyName).ToList(); // Get the faculty names
                var allFaculties = _context.Faculties.Select(f => f.FacultyName).Distinct().ToList(); // Get all distinct faculties

                var model = new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    Roles = userRoles,
                    AllRoles = allRoles, // Set all roles to "Guest"
                    Faculties = faculties, // Set faculties
                    AllFaculties = allFaculties, // Set all distinct faculties
                    NumberOfContributions = user.NumberOfContributions // Set NumberOfContributions
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
                user.NumberOfContributions = model.NumberOfContributions; // Update NumberOfContributions

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

                // Get the current user faculties
                var currentFaculties = _context.Faculties.Where(f => f.Users.Any(u => u.Id == user.Id)).Select(f => f.FacultyName).ToList();

                // Determine faculties to be added (faculties in the model that are not in the current faculties)
                var facultiesToAdd = model.Faculties.Except(currentFaculties);

                // Determine faculties to be removed (faculties in the current faculties that are not in the model)
                var facultiesToRemove = currentFaculties.Except(model.Faculties);

                // Add new faculties
                foreach (var faculty in facultiesToAdd)
                {
                    var facultyInDb = _context.Faculties.FirstOrDefault(f => f.FacultyName == faculty);

                    if (facultyInDb != null)
                    {
                        if (facultyInDb.Users == null)
                        {
                            facultyInDb.Users = new List<ApplicationUser>();
                        }
                        facultyInDb.Users.Add(user);
                    }

                }

                // Remove faculties not in the model
                foreach (var faculty in facultiesToRemove)
                {
                    var facultyInDb = _context.Faculties.FirstOrDefault(f => f.FacultyName == faculty);
                    if (facultyInDb != null)
                    {
                        facultyInDb.Users.Remove(user);
                    }
                }

                // Save changes
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    AddErrors(updateResult);
                    return View(model);
                }

                await _context.SaveChangesAsync(); // Save changes in the database

                return RedirectToAction("Index");
            }

            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            public async Task<IActionResult> Delete(string id)
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

            // POST: /Account/DeleteConfirmed
            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return BadRequest("No current user found");
                }

                if (currentUser.Id == user.Id)
                {
                    return BadRequest("Cannot delete the currently logged in admin account");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to delete user");
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }


