namespace FoodRecipes.Models.Recipes
{
    public class DetailsRecipeQueryModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string Ingredients { get; init; }

        public string Directions { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }

        public int CookingTime { get; init; }
    }
}
