namespace FoodRecipes.Models.Home
{
    using System.Collections.Generic;
    using FoodRecipes.Services.Recipes.Models;

    public class IndexViewModel
    {
        public int TotalRecipes { get; init; }

        public int TotalUsers { get; init; }

        public int TotalCooks { get; init; }

        public IList<LatestRecipeServiceModel> Recipes { get; init; }
    }
}
