namespace FoodRecipes.Services.Ingredients
{
    using System.Collections.Generic;
    using FoodRecipes.Models;

    public interface IIngredientService
    {
        IngredientQueryServiceModel All(string name,
            string searchTerm,
            IngredientSorting sorting,
            int currentPage,
            int ingredientsPerPage);

        IngredientDetailsServiceModel Details(int recipeId);

        int Create(
            string name,
            string imageUrl,
            int categoryId,
            int sellerId);

        bool Edit(
            int ingredientId,
            string name,
            string imageUrl,
            int categoryId);

        IEnumerable<IngredientServiceModel> GetIngredientsByUser(string userId);

        bool IngredientIsFromSeller(int ingredientId, int sellerId);

        IEnumerable<string> AllIngredientNames();

        IEnumerable<IngredientCategoryServiceModel> AllIngredientCategories();

        bool CategoryExists(int categoryId);
    }
}
