using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _1640WebDevUMC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base implementation

            // Configure keyless entity type for SelectListItem
            modelBuilder.Entity<SelectListGroup>().HasNoKey();
        }
        public DbSet<Account>? Accounts { get; set; }

    }
}
