namespace FoodRecipes.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalRecipes { get; init; }

        public int TotalUsers { get; init; }

        public List<RecipeIndexViewModel> Recipes { get; init; }
    }
}
