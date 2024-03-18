using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _1640WebDevUMC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AcademicYear>? AcademicYears { get; set; }
        public DbSet<Contribution>? Contributions { get; set; }
        public DbSet<Faculty>? Faculties { get; set; }
        public DbSet<DownloadHistory>? DownloadHistories { get; set; }
        public DbSet<_1640WebDevUMC.Models.File>? Files { get; set; } // Fully qualify the namespace to resolve ambiguity
        public DbSet<Image>? Images { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<Accounts>? Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure keyless entity type for SelectListItem
            modelBuilder.Entity<SelectListGroup>().HasNoKey();
        }
    }
}
