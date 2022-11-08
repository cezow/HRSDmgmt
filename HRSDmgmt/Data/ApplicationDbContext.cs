using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRSDmgmt.Models;

namespace HRSDmgmt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            ModelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .OnDelete(DeleteBehavior.Restrict);

            ModelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .OnDelete(DeleteBehavior.Restrict);

            ModelBuilder.Entity<Company>()
                .HasMany(c => c.Offers)
                .WithOne(o => o.Company)
                .OnDelete(DeleteBehavior.Restrict);


            ModelBuilder.Entity<Offer>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Offers)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}