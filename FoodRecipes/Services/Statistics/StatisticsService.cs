namespace FoodRecipes.Services.Statistics
{
    using System.Linq;
    using FoodRecipes.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly FoodRecipesDbContext data;

        public StatisticsService(FoodRecipesDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalRecipes = this.data.Recipes.Count();
            var totalUsers = this.data.Users.Count();
            var totalCooks = this.data.Cooks.Count();

            return new StatisticsServiceModel
            {
                TotalRecipes = totalRecipes,
                TotalUsers = totalUsers,
                TotalCooks = totalCooks,
            };
        }
    }
}
