using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using HRSDmgmt.Models;

namespace HRSDmgmt.Data
{
    public class DataSeeder
    {
        public static void Inizialize(IServiceProvider serviceprovider)
        {
            using (var dbContext = new ApplicationDbContext(
                serviceprovider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))

                if (dbContext.Database.CanConnect())
                {
                    SeedRoles(dbContext);
                    SeedUsers(dbContext);
                    SeedCompanies(dbContext);
                    SeedOffers(dbContext);
                    SeedEmployees(dbContext);
                }
        }

        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "user"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "company"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "employee"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user"
                }).Wait();
            }
        }
        private static void SeedUsers(ApplicationDbContext dbContext)
        {

            if (!dbContext.Users.Any(u => u.UserName == "admin"))
            {
                var user = new AppUser
                {
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "admin@firm.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Adam",
                    LastName = "Ruciński",
                    Photo = "user1.jpg",
                    Information = "Guru informatyczny w firmie. Wielu kolegów z pracy uważa, że jest kosmitą z innej planety."
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Firmowe1!");
                user.PasswordHash = hashed;
                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "admin").Wait();
                dbContext.SaveChanges();
            }
            if (!dbContext.Users.Any(u => u.UserName == "user"))
            {
                var user = new AppUser
                {
                    UserName = "user",
                    NormalizedUserName = "user",
                    Email = "user@firma.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    FirstName = "Anna",
                    LastName = "Sekretarska",
                    Photo = "user2.png",
                    Information = "Najpilniejsza z najpilniejszych. Poprostu bezbłędna"
                };
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Firmowe1!");
                user.PasswordHash = hashed;
                var userStore = new UserStore<AppUser>(dbContext);
                userStore.CreateAsync(user).Wait();
                userStore.AddToRoleAsync(user, "user").Wait();
                dbContext.SaveChanges();
            }
            for (int i=1; i>=2; i++)
            {
                if (!dbContext.Users.Any(u => u.UserName == "company"+ i))
                {
                    var user = new AppUser
                    {
                        UserName = "company" + i,
                        NormalizedUserName = "company" + i,
                        Email = "company" + i + "@firma.pl",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        FirstName = "Imię" + i,
                        LastName = "Nazwisko" + i,
                        Photo = "user" + (i + 2) + ".png",
                        Information = "Monopolista na rynku"
                    };
                    var password = new PasswordHasher<AppUser>();
                    var hashed = password.HashPassword(user, "Firmowe1!");
                    user.PasswordHash = hashed;
                    var userStore = new UserStore<AppUser>(dbContext);
                    userStore.CreateAsync(user).Wait();
                    userStore.AddToRoleAsync(user, "company").Wait();
                    dbContext.SaveChanges();
                }
            }
            for (int i=1; i>=5; i++)
            {
                if (!dbContext.Users.Any(u => u.UserName == "company" + i))
                {
                    var user = new AppUser
                    {
                        UserName = "pracownik" + i,
                        NormalizedUserName = "company" + i,
                        Email = "company" + i + "@firma.pl",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        FirstName = "Imię" + (i + 2),
                        LastName = "Nazwisko" + (i + 2),
                        Photo = "user5.png",
                        Information = "Kolejna nieperspektywiczna osoba z dużymi wymaganiami szukająca pracy"
                    };
                    var password = new PasswordHasher<AppUser>();
                    var hashed = password.HashPassword(user, "Firmowe1!");
                    user.PasswordHash = hashed;
                    var userStore = new UserStore<AppUser>(dbContext);
                    userStore.CreateAsync(user).Wait();
                    userStore.AddToRoleAsync(user, "employee").Wait();
                    dbContext.SaveChanges();
                }
            }
        }
        private static void SeedCategories(ApplicationDbContext dbContext)
        {
            if (!dbContext.Companies.Any())
            {
                var company = new List<Company>
                {
                    new Company
                    {
                        Name = "Wiadomości",
                        Active = true,
                        Display=true,
                        Icon="chat-left-text",
                        Description="Najświeższe wiadomości i informacje z dziedziny informatyki. Coś dla programistów i zwykłych użytkowników komputerów, tabletów oraz smartfonów."
                    },

                    new Category
                    {
                        Name = "Artykuły",
                        Active = true,
                        Display=true,
                        Icon="journal-richtext",
                        Description="Artykuły w naszym serwisie pisane są przez wybitnych znawców tematu, którzy z olbrzymią przenikliwością zgłębiają każdy temat."
                    },
                    new Category
                    {
                        Name = "Testy",
                        Active = true,
                        Display=true,
                        Icon="speedometer",
                        Description="Nasze laboratorium testuje dla Was najnowszy sprzęt, poddając go elektronicznym torturom i wyciskając siódme poty elektronów."
                    },
                    
                };
                dbContext.AddRange(company);
                dbContext.SaveChanges();
            }
        }
    }
}
