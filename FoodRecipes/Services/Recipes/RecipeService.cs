namespace FoodRecipes.Services.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using FoodRecipes.Data;
    using FoodRecipes.Models;

    public class RecipeService : IRecipeService
    {
        private readonly FoodRecipesDbContext data;

        public RecipeService(FoodRecipesDbContext data)
            => this.data = data;

        public RecipeQueryServiceModel All(
            string name,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                recipesQuery = recipesQuery.
                    Where(r => r.Name == name);
            }


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Ingredients.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Category.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Directions.ToLower().Contains(searchTerm.ToLower()));
            }

            recipesQuery = sorting switch
            {
                RecipeSorting.Name => recipesQuery.OrderBy(r => r.Name),
                RecipeSorting.Category => recipesQuery.OrderBy(r => r.Category.Name),
                RecipeSorting.CookingTime => recipesQuery.OrderBy(r => r.CookingTime),
                RecipeSorting.DateCreated or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage)
                .Select(r => new RecipeServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CookingTime = r.CookingTime,
                    Category = r.Category.Name,
                    ImageUrl = r.ImageUrl,
                })
                .ToList();

            return new RecipeQueryServiceModel
            {
                CurrentPage = currentPage,
                RecipesPerPage = recipesPerPage,
                TotalRecipes = totalRecipes,
                Recipes = recipes,
            };
        }

        public IEnumerable<string> AllRecipesNames()
            => this.data
                .Recipes
                .Select(r => r.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        //public IActionResult Details(int recepiId) // WIP
        //{
        //    var recipe = this.data
        //        .Recipes
        //        .Where(r => r.Id == recepiId)
        //        .FirstOrDefault();

        //    if (recipe == null)
        //    {
        //        return View(new RecipeDetalsServiceModel
        //        {
        //            Name = "invalid",
        //            CookingTime = 0,
        //        });
        //    }

        //    var recipeToShow = new RecipeDetalsServiceModel
        //    {
        //        Name = recipe.Name,
        //        Ingredients = recipe.Ingredients,
        //        Directions = recipe.Directions,
        //        ImageUrl = recipe.ImageUrl,
        //        Category = recipe.Category.Name,
        //        CookingTime = recipe.CookingTime
        //    };

        //    return View(recipeToShow);
        //}
    }
}
