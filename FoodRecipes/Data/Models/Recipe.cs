namespace FoodRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.RecipeConstants;

    public class Recipe
    {
        public int Id { get; init; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(IngredientsMinLength)]
        public string Ingredients { get; set; }

        [Required]
        [MinLength(DirectionsMinLength)]
        public string Directions { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CookingTime { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public RecipeCategory Category { get; set; }

        public int CookId { get; init; }

        public Cook Cook { get; init; }
    }
}
