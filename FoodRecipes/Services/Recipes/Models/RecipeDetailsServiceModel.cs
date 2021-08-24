namespace FoodRecipes.Services.Recipes.Models
{
    public class RecipeDetailsServiceModel : RecipeServiceModel
    {
        public string Ingredients { get; init; }

        public string Directions { get; init; }

        public string UserId { get; init; }

        public int CookId { get; init; }

        public int CategoryId { get; init; }

        public string CookName { get; init; }
    }
}
