namespace FoodRecipes.Infrastructure
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using FoodRecipes.Data;
    using FoodRecipes.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<FoodRecipesDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(FoodRecipesDbContext data)
        {
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
    }
}
