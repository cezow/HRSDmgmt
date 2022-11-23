using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using HRSDmgmt.Models;
using System.Reflection;
using System.Xml.Linq;
using System;

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
                    Name = "company",
                    NormalizedName = "company"
                }).Wait();
            }

            if (!dbContext.Roles.Any(r => r.Name == "employee"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "employee",
                    NormalizedName = "employee"
                }).Wait();
            }
        }
        private static void SeedUsers(ApplicationDbContext dbContext)
        {

            if (!dbContext.Users.Any(u => u.UserName == "admin"))
            {
                var user = new AppUser
                {
                    UserName = "admin@firma.pl",
                    NormalizedUserName = "admin@firma.pl",
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
                    UserName = "user@firma.pl",
                    NormalizedUserName = "user@firma.pl",
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
                if (!dbContext.Users.Any(u => u.UserName == "company"+ i.ToString()))
                {
                    var user = new AppUser
                    {
                        UserName = "company" + i.ToString() + "@firma.pl",
                        NormalizedUserName = "company" + i.ToString() + "@firma.pl",
                        Email = "company" + i.ToString() + "@firma.pl",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        FirstName = "Imię" + i.ToString(),
                        LastName = "Nazwisko" + i   ,
                        Photo = "user" + (i + 2).ToString() + ".png",
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
                if (!dbContext.Users.Any(u => u.UserName == "company" + i.ToString()))
                {
                    var user = new AppUser
                    {
                        UserName = "pracownik" + i.ToString() + "@firma.pl",
                        NormalizedUserName = "company" + i.ToString() + "@firma.pl" ,
                        Email = "company" + i.ToString() + "@firma.pl",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        FirstName = "Imię" + (i + 2).ToString(),
                        LastName = "Nazwisko" + (i + 2).ToString(),
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
        private static void SeedCompanies(ApplicationDbContext dbContext)
        {
            if (!dbContext.Companies.Any())
            {
                var company = new List<Company>
                {
                    new Company
                    {
                        Name = "Stocznia Gdańska",
                        NIP = 1234567890,
                        Description="jedna z największych polskich stoczni, zlokalizowana w Gdańsku na lewym brzegu Martwej Wisły i na Ostrowiu." +
                        " Powstała po 1945 na terenach, gdzie wcześniej istniały niemieckie stocznie Jana Klawittera (od 1804), następnie Kaiserliche" +
                        " Werft Danzig (od 1844) oraz Schichau (od 1890). Stocznia Gdańska w ciągu swojej działalności zbudowała ponad 1000 w pełni" +
                        " wyposażonych statków pełnomorskich, m.in.: kontenerowców, statków pasażerskich i żaglowców. Na jej terenie miało miejsce" +
                        " stłumienie protestów oraz zamordowanie trzech stoczniowców – ofiar wydarzeń Grudnia 1970. Z gdańskiej stoczni wywodzi się NSZZ „Solidarność”," +
                        " na terenie zakładu podpisano porozumienia sierpniowe w 1980 roku. W 1996 roku postawiona w stan upadłości, następnie na bazie przedsiębiorstwa" +
                        " powstała Stocznia Gdańska – Grupa Stoczni Gdynia SA, od 2006 roku – Stocznia Gdańsk SA.",
                        Address="Na Ostrowiu 15/20, 80-873 Gdańsk",
                        Country="Polska",
                        ContactPerson="Jan Rybak",
                        Mobile="(+48) 658-778-114",
                        Email="jan.rybak@stocznia.pl",
                        Logo="logo1.png",
                        Active=true,
                        Display=true
                    },

                    new Company
                    {
                        Name = "PolService",
                        NIP = 1234567891,
                        Description="Przedsiębiorstwo budowlane",
                        Address="Skrajna 1, 22-233 Poznań",
                        Country="Polska",
                        ContactPerson="Zenon Artysta",
                        Mobile="(+48) 678-444-242",
                        Email="za@polservice.pl",
                        Logo="logo2.png",
                        Active=true,
                        Display=true
                    },
                  
                };
                dbContext.AddRange(company);
                dbContext.SaveChanges();
            }
        }
        private static void SeedOffers(ApplicationDbContext dbContext)
        {
            if (!dbContext.Offers.Any())
            {
                for (int i = 1; i <= 6; i++)   
                {
                    var company1_id = dbContext.Companies.Where(c => c.CompanyId == 1).FirstOrDefault().CompanyId;
                    var company2_id = dbContext.Companies.Where(c => c.CompanyId == 2).FirstOrDefault().CompanyId;

                    var offer = new Models.Offer()
                        {
                            Name        = "Oferta",
                            Description = "Umowa o pracę w firmie" + (i<=4 ? "Stocznia Gdańska" : "PolService"),
                            Vacancy     = 2,
                            StartDate   = DateTime.Now.AddMonths(i),
                            EndDate     = DateTime.Now.AddMonths(i+12),
                            CompanytId  = i<=4 ? company1_id : company2_id,
                            Active      = true,
                            Display     =true
                        };
                        dbContext.Set<Models.Offer>().Add(offer);
                }
                dbContext.SaveChanges();
            }
        }

        private static void SeedEmployees(ApplicationDbContext dbContext)
        {
            if (!dbContext.Employees.Any())
            {
                var company1_id = dbContext.Companies.Where(c => c.CompanyId == 1).FirstOrDefault().CompanyId;
                var company2_id = dbContext.Companies.Where(c => c.CompanyId == 2).FirstOrDefault().CompanyId;

                for (int i=0; i<=8; i++)
                {
                    var employee = new Models.Employee()
                    {
                        FirstName   = "Imię" + i.ToString(),
                        LastName    = "Nazwisko" + i.ToString(),
                        Mobile      = "(+48) 888-456-00" + i.ToString(),
                        Email       = "Imię" + i.ToString() + "@hotmail.com",
                        Education   = "Szkoła zawodowa",
                        Profession  = i<=3 ? "spawacz" : "monter",
                        Skills      = i <= 3 ? "spawanie metodą TIG 141 i elekrodą 111" : "znajomość rysunku izometrycznego",
                        Experience  = i <= 3 ? "12 lat doświadczenia w pracach jako spawacz" : "montaż rurociągów i konstrukcji stalowych, praca na stoczni",
                        CV          = "cv" + i + ".pdf",
                    };
                    dbContext.Set<Models.Employee>().Add(employee);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
