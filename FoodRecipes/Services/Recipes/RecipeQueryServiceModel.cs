namespace FoodRecipes.Services.Recipes
{
    using System.Collections.Generic;
    using FoodRecipes.Models.Api.Recipes;

    public class RecipeQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int RecipesPerPage { get; init; }

        public int TotalRecipes { get; init; }

        public IEnumerable<RecipeServiceModel> Recipes { get; init; }
    }
}
