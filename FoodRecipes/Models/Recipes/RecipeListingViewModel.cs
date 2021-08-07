namespace FoodRecipes.Models.Recipes
{
    public class RecipeListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int CookingTime { get; init; }

        public string Category { get; init; }

        public string ImageUrl { get; init; }
    }
}
