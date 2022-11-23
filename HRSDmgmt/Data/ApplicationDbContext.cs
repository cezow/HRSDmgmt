using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRSDmgmt.Models;
using Microsoft.CodeAnalysis.Options;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

namespace HRSDmgmt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Offer> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           /* modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .OnDelete(DeleteBehavior.Restrict); */

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Offers)
                .WithOne(o => o.Company)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Offers)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Company)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Company>()
                .HasOne(c => c.User)
                .WithOne(u => u.Company)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}