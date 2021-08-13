namespace FoodRecipes.Services.Recipes
{
    using System.Collections.Generic;
    using FoodRecipes.Models;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(string name,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        IEnumerable<string> AllRecipesNames();
    }
}
