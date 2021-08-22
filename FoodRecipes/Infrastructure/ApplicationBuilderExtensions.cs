namespace FoodRecipes.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using FoodRecipes.Data;
    using FoodRecipes.Data.Models;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }
        
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<FoodRecipesDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<FoodRecipesDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Soup" },
                new Category {Name = "Salad" },
                new Category {Name = "Appetizer" },
                new Category {Name = "Side Dish" },
                new Category {Name = "Main Dish" },
                new Category {Name = "Breakfast" },
                new Category {Name = "Baked Goods" },
                new Category {Name = "Dessert" },
                new Category {Name = "Baby Food" },
                new Category {Name = "Pasta" },
                new Category {Name = "Canned Food" }
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@foodrecipes.com";
                    const string adminPassword = "adminadmin";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName= adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();


        }
    }
}
