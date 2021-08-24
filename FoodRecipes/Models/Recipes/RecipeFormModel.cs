namespace FoodRecipes.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FoodRecipes.Services.Recipes.Models;
    using static Data.DataConstants.RecipeConstants;

    public class RecipeFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength, 
            MinimumLength = NameMinLength, 
            ErrorMessage = "Name bust be between {2} and {1} characters.")]
        public string Name { get; init; }

        [Required]
        [StringLength(
            IngredientsMaxLength, 
            MinimumLength = IngredientsMinLength, 
            ErrorMessage = "Ingredients list bust be between {2} and {1} characters.")]
        public string Ingredients { get; init; }

        [Required]
        [StringLength(
            DirectionsMaxLength, 
            MinimumLength = DirectionsMinLength, 
            ErrorMessage = "Directions list bust be between {2} and {1} characters.")]
        public string Directions { get; init; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Please enter a valid URL.")]
        [Url]
        public string ImageUrl { get; init; }

        [Range(MinCookingTime, MaxCookingTime, ErrorMessage = "Cooking time must be between {1} and  {2} minutes!")]
        [Display(Name = "Cook Time")]
        public int CookingTime { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<RecipeCategoryServiceModel> Categories { get; set; }
    }
}
