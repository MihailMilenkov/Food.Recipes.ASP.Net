namespace FoodRecipes.Services.Cooks
{
    using System.Linq;
    using FoodRecipes.Data;

    public class CookService : ICookService
    {
        private readonly FoodRecipesDbContext data;

        public CookService(FoodRecipesDbContext data)
        {
            this.data = data;
        }

        public bool IsCook(string userId)
            => this.data
                   .Cooks
                   .Any(c => c.UserId == userId);

        public int GetCookIdByUserId(string userId)
            => this.data
                .Cooks
                .Where(c => c.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
