namespace FoodRecipes.Services.Ingredients
{
    using System.Collections.Generic;
    using FoodRecipes.Models.Api.Recipes;

    public class IngredientQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int IngredientsPerPage { get; init; }

        public int TotalIngredients { get; init; }

        public IEnumerable<IngredientServiceModel> Ingredients { get; init; }
    }
}
