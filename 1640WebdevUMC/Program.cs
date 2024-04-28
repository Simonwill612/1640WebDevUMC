using _1640WebDevUMC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
/*builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IImageService, ImageService>();*/
/*builder.Services.AddScoped<IContributionItemService, ContributionItemService>();
*/
builder.Services.AddTransient<IEmailSender, EmailService>();

builder.Services.AddScoped<EmailService>();
/*builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

/*app.MapControllerRoute(
       name: "studentDetails",
    pattern: "Student/Details/{contributionId}/{id?}", // Adjusted route template
    defaults: new { controller = "Student", action = "Details" });
*/
app.MapControllerRoute(
    name: "guestIndex",
    pattern: "/Guest", // Specify the URL pattern for the Guest controller index action
    defaults: new { controller = "Guest", action = "Index" } // Specify the controller and action
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Assign roles to users
    await AssignRolesToUsers(userManager, roleManager);
}

app.Run();

static async Task AssignRolesToUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // List the roles
    var roles = new[]
    {
            "Marketing Coordinator",
            "Guest",
            "Student",
            "Admin",
            "Marketing Managerment"
        };

    foreach (var role in roles)
    {
        // Check if the role already exists, if not, create a new one
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Map roles to users
    var usersRolesMapping = new Dictionary<string, string[]>
        {
            { "MarketingManagerment1@gmail.com", new[] { "Marketing Managerment" } },
            { "Guest1@gmail.com", new[] { "Guest" } },
            { "MarketingCoodinator1@gmail.com", new[] { "Marketing Coordinator" } },
            { "Student1@gmail.com", new[] { "Student" } },
            { "MarketingManagerment@gmail.com", new[] { "Marketing Managerment" } },
            { "Student@gmail.com", new[] { "Student" } },
            { "Admin@gmail.com", new[] { "Admin" } },
            { "Admin1@gmail.com", new[] { "Admin" } },
            { "MarketingCoodinator@gmail.com", new[] { "Marketing Coordinator" } }
        };

    foreach (var mapping in usersRolesMapping)
    {
        var user = await userManager.FindByEmailAsync(mapping.Key);
        if (user != null)
        {
            foreach (var roleName in mapping.Value)
            {
                // Check role existence and mapping for user
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
