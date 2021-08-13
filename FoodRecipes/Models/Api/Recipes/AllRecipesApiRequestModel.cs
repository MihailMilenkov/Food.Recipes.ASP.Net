namespace FoodRecipes.Models.Api.Recipes
{
    public class AllRecipesApiRequestModel
    {
        public string Name { get; set; }

        public string SearchTerm { get; init; }

        public RecipeSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int RecipesPerPage { get; init; } = 4;

        public int TotalRecipes { get; init; }
    }
}
