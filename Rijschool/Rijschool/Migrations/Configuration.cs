namespace Rijschool.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Rijschool.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rijschool.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rijschool.Models.ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            roleManager.Create(new IdentityRole { Name = "Klant" });
            roleManager.Create(new IdentityRole { Name = "Instructeur" });
            roleManager.Create(new IdentityRole{Name="Admin"});
            
            int i = 1;
            while (i<=57)
            {
                var user = new Klant
                {
                    Voornaam = String.Concat("Klant", i),
                    Familienaam = String.Concat("Fam", i),
                    Adres = String.Concat("Straat ", i),
                    Gemeente = String.Concat("Gemeente ", i),
                    Email = String.Concat("klant", i , "@example.com"),
                    UserName = String.Concat("klant", i, "@example.com"),
                    KlantSedert = DateTime.Now.Date
                };

                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "Klant");
                i++;
            }

            i = 1;
            while (i <= 23)
            {
                var user = new Instructeur
                {
                    Voornaam = String.Concat("Instructeur ", i),
                    Familienaam = String.Concat("Fam ", i),
                    Adres = String.Concat("Straat ", i),
                    Gemeente = String.Concat("Gemeente ", i),
                    Email = String.Concat("instructeur", i, "@example.com"),
                    UserName = String.Concat("instructeur", i, "@example.com")
                };

                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "Instructeur");
                i++;
            }

            var admin = new Personeel
            {
                Voornaam = "AdminVoornaam",
                Familienaam = "AdminAchternaam",
                Adres = "Adminlaan 1",
                Gemeente = "AdminGemeente",
                Email = "admin@example.com",
                UserName = "admin@example.com"
            };
            userManager.Create(admin, "password");
            userManager.AddToRole(admin.Id, "Admin");
            i++;
        }
    }
}
