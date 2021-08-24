namespace FoodRecipes.Services.Recipes.Models
{
    public class RecipeServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int CookingTime { get; init; }

        public string CategoryName { get; init; }

        public string ImageUrl { get; init; }
    }
}
