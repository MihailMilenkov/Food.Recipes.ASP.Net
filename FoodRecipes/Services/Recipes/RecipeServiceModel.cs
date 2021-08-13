namespace FoodRecipes.Services.Recipes
{
    public class RecipeServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int CookingTime { get; init; }

        public string Category { get; init; }

        public string ImageUrl { get; init; }
    }
}
