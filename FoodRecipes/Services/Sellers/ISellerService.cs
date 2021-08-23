namespace FoodRecipes.Services.Sellers
{
    public interface ISellerService
    {
        public bool IsSeller(string userId);

        public int GetSellerIdByUserId(string userId);
    }
}
