namespace FoodRecipes.Services.Ingredients
{
    public class IngredientDetailsServiceModel : IngredientServiceModel
    {

        public string UserId { get; init; }

        public int SellerId { get; init; }

        public int CategoryId { get; init; }

        public string SellerName { get; init; }
    }
}
