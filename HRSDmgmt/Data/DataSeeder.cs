using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using HRSDmgmt.Models;
using System.Reflection;

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
                if (!dbContext.Users.Any(u => u.UserName == "company"+ i.ToString()))
                {
                    var user = new AppUser
                    {
                        UserName = "company" + i.ToString(),
                        NormalizedUserName = "company" + i.ToString(),
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
                        UserName = "pracownik" + i.ToString(),
                        NormalizedUserName = "company" + i.ToString(),
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
                    var company1_id = dbContext.AppUsers.Where(u => u.UserName == "company1").FirstOrDefault().Id;


                        var offer = new Models.Offer()
                        {
                            
                        };
                        dbContext.Set<Models.Offer>().Add(offer);
                    }
                    dbContext.SaveChanges();

                    /* var idUzytkownika2 = dbContext.AppUsers.Where(u => u.UserName == "autor2@portal.pl").FirstOrDefault().Id;

                    for (int j = 5; j <= 9; j++) //teksty autora2
                    {
                        var tekst = new Models.Text()
                        {
                            Title = "Tytuł" + i.ToString() + j.ToString(),
                            Summary = "Streszczenie tekstu o tytule Title" + i.ToString() + j.ToString(),
                            Keywords = "tag" + j.ToString() + ", tag" + (i + j).ToString(),
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" +
                                " incididunt ut labore et dolore magna aliqua.Ut enim ad minim veniam, quis nostrud exercitation" +
                                " ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit" +
                                " in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat" +
                                " non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.Sed ut perspiciatis" +
                                " unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam," +
                                " eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo." +
                                " Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur" +
                                " magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum" +
                                " quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut" +
                                " labore et dolore magnamaliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem" +
                                " ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure" +
                                " reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem" +
                                " eum fugiat quo voluptas nulla pariatur ? ",
                            AddedDate = DateTime.Now.AddDays(-i * j),
                            CategoryId = i,
                            Id = idUzytkownika2,
                            Active = true
                        };
                        dbContext.Set<Models.Text>().Add(tekst);
                    }
                    dbContext.SaveChanges(); */
                }
            }
        }
    }
}
