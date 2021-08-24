namespace FoodRecipes.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FoodRecipes.Models;
    using FoodRecipes.Services.Recipes.Models;

    public class AllRecipesQueryModel
    {
        public const int RecipesPerPage = 4;

        [Display(Name = "Choose Name:")]
        public string Name { get; set; }

        [Display(Name = "Search by name, type, directions or ingredients:")]
        public string SearchTerm { get; init; }

        [Display(Name = "Sort by:")]
        public RecipeSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalRecipes { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<RecipeServiceModel> Recipes { get; set; }
    }
}
