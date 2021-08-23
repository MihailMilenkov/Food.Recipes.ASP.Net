namespace FoodRecipes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.IngredientConstants;

    public class IngredientCategory
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; init; } = new List<Ingredient>();
    }
}
