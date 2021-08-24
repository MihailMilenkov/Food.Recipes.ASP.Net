namespace FoodRecipes.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FoodRecipes.Services.Recipes.Models;
    using static Data.DataConstants.RecipeConstants;

    public class AddRecipeFormModel
    {
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(
            NameMaxLength, 
            MinimumLength = NameMinLength, 
            ErrorMessage = "The name bust be between {2} and {1} characters.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "The ingredients list is required.")]
        [StringLength(
            IngredientsMaxLength, 
            MinimumLength = IngredientsMinLength, 
            ErrorMessage = "The ingredients list bust be between {2} and {1} characters.")]
        public string Ingredients { get; init; }

        [Required(ErrorMessage = "The directions list is required.")]
        [StringLength(
            DirectionsMaxLength, 
            MinimumLength = DirectionsMinLength, 
            ErrorMessage = "The directions list bust be between {2} and {1} characters.")]
        public string Directions { get; init; }

        [Required(ErrorMessage = "Please enter a valid URL.")]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        [Range(
            MinCookingTime, 
            MaxCookingTime, 
            ErrorMessage = "The Cooking time must be between {1} and  {2} minutes!")]
        [Display(Name = "Cooking Time")]
        public int CookingTime { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<RecipeCategoryServiceModel> Categories { get; set; }
    }
}
