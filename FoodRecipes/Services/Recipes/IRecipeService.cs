namespace FoodRecipes.Services.Recipes
{
    using System.Collections.Generic;
    using FoodRecipes.Models;
    using FoodRecipes.Services.Recipes.Models;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(string name,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage);

        IEnumerable<LatestRecipeServiceModel> Latest();

        RecipeDetailsServiceModel Details(int recipeId);

        int Create(
            string name,
            string ingredients,
            string directions,
            string imageUrl,
            int cookingTime,
            int categoryId,
            int cookId);

        bool Edit(
            int recipeId,
            string name,
            string ingredients,
            string directions,
            string imageUrl,
            int cookingTime,
            int categoryId);

        IEnumerable<RecipeServiceModel> GetRecipesByUser(string userId);

        bool RecipeIsFromCook(int recipeId, int cookId);

        IEnumerable<string> AllRecipeNames();

        IEnumerable<RecipeCategoryServiceModel> AllRecipeCategories();

        bool CategoryExists(int categoryId);
    }
}
