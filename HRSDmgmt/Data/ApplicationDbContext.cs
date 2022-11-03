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

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            ModelBuilder.Entity<Client>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Client);

            ModelBuilder.Entity<Employee>()
                .HasOne(e => e.Client)
                .WithMany(c => c.Employees);

            ModelBuilder.Entity<Client>()
                .HasMany(c => c.Offers)
                .WithOne(o => o.Client);

            ModelBuilder.Entity<Offer>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Offers);
        }
    }
}