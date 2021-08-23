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

            SeedRecipeCategories(services);
            SeedIngredientCategories(services);
            SeedAdministrator(services);

            return app;
        }
        
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<FoodRecipesDbContext>();

            data.Database.Migrate();
        }

        private static void SeedRecipeCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<FoodRecipesDbContext>();

            if (data.RecipeCategories.Any())
            {
                return;
            }

            data.RecipeCategories.AddRange(new[]
            {
                new RecipeCategory {Name = "Soup" },
                new RecipeCategory {Name = "Salad" },
                new RecipeCategory {Name = "Appetizer" },
                new RecipeCategory {Name = "Side Dish" },
                new RecipeCategory {Name = "Main Dish" },
                new RecipeCategory {Name = "Breakfast" },
                new RecipeCategory {Name = "Baked Goods" },
                new RecipeCategory {Name = "Dessert" },
                new RecipeCategory {Name = "Baby Food" },
                new RecipeCategory {Name = "Pasta" },
                new RecipeCategory {Name = "Canned Food" }
            });

            data.SaveChanges();
        }

        private static void SeedIngredientCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<FoodRecipesDbContext>();

            if (data.IngredientCategories.Any())
            {
                return;
            }

            data.IngredientCategories.AddRange(new[]
            {
                new IngredientCategory {Name = "Eggs, milk and milk products" },
                new IngredientCategory {Name = "Fats and oils" },
                new IngredientCategory {Name = "Fruits" },
                new IngredientCategory {Name = "Vegetables" },
                new IngredientCategory {Name = "Grain, nuts and baking products" },
                new IngredientCategory {Name = "Herbs and spices" },
                new IngredientCategory {Name = "Meat" },
                new IngredientCategory {Name = "Fish" },
                new IngredientCategory {Name = "Pasta, rice and pulses" },
                new IngredientCategory {Name = "Other" },
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
