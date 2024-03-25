using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _1640WebDevUMC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<DownloadHistory> DownloadHistories { get; set; }
        public DbSet<_1640WebDevUMC.Models.File> Files { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.AcademicYears)
                .WithOne(a => a.Faculty)
                .HasForeignKey(a => a.FacultyID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(a => a.Contributions)
                .WithOne(c => c.AcademicYear)
                .HasForeignKey(c => c.YearID);

            modelBuilder.Entity<Contribution>()
                .HasMany(c => c.Files)
                .WithOne(f => f.Contribution)
                .HasForeignKey(f => f.ContributionID);

            modelBuilder.Entity<Contribution>()
                .HasMany(c => c.Images)
                .WithOne(i => i.Contribution)
                .HasForeignKey(i => i.ContributionID);

            modelBuilder.Entity<Contribution>()
                .HasMany(c => c.DownloadHistories)
                .WithOne(d => d.Contribution)
                .HasForeignKey(d => d.ContributionID);

            modelBuilder.Entity<Contribution>()
                .HasMany(c => c.Notifications)
                .WithOne(n => n.Contribution)
                .HasForeignKey(n => n.ContributionID);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Contributions)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey(c => c.UserID);
        }
    }
}
