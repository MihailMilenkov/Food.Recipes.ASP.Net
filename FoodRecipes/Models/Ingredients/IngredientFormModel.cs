namespace FoodRecipes.Models.Ingredients
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FoodRecipes.Services.Ingredients;

    using static Data.DataConstants.IngredientConstants;

    public class IngredientFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength, 
            MinimumLength = NameMinLength, 
            ErrorMessage = "Name bust be between {2} and {1} characters.")]
        public string Name { get; init; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Please enter a valid URL.")]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<IngredientCategoryServiceModel> Categories { get; set; }
    }
}
