namespace FoodRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.IngredientConstants;

    public class Ingredient
    {
        public int Id { get; init; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public IngredientCategory Category { get; set; }

        public int SellerId { get; init; }

        public Seller Seller { get; init; }
    }
}
