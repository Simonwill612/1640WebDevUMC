using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = _1640WebDevUMC.Models.File;
using Image = _1640WebDevUMC.Models.Image;


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
        /*        public DbSet<ContributionItems> ContributionItems { get; set; }
        */
/*        public DbSet<ContributionWithFile> ContributionsWithFiles { get; set; }
*/
        public DbSet<Comment> Comments { get; set; } // Uncommented DbSet

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<DownloadHistory> DownloadHistories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

/*            modelBuilder.Entity<ContributionWithFiles>().HasNoKey();
*/
            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.AcademicYears)
                .WithOne(a => a.Faculty)
                .HasForeignKey(a => a.FacultyID);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Contribution)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ContributionID)
                .OnDelete(DeleteBehavior.NoAction);

            /*            modelBuilder.Entity<Comment>()
                            .HasOne(c => c.ApplicationUser)
                            .WithMany()
                            .HasForeignKey(c => c.Email)
                            .OnDelete(DeleteBehavior.Restrict);*/

            /*            modelBuilder.Entity<Comment>()
                            .HasOne(c => c.ContributionItem)
                            .WithMany()
                            .HasForeignKey(c => c.ContributionItemID)
                            .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}
