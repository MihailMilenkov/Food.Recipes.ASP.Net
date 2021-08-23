namespace FoodRecipes.Services.Sellers
{
    using System.Linq;
    using FoodRecipes.Data;

    public class SellerService : ISellerService
    {
        private readonly FoodRecipesDbContext data;

        public SellerService(FoodRecipesDbContext data)
        {
            this.data = data;
        }

        public bool IsSeller(string userId)
            => this.data
                   .Sellers
                   .Any(c => c.UserId == userId);

        public int GetSellerIdByUserId(string userId)
            => this.data
                .Sellers
                .Where(c => c.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
