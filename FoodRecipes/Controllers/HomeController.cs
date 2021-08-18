namespace FoodRecipes.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Services;
    using FoodRecipes.Data;
    using FoodRecipes.Models.Home;
    using FoodRecipes.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly FoodRecipesDbContext data;

        public HomeController(IStatisticsService statistics, FoodRecipesDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var recipes = this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .Select(r => new RecipeIndexViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CookingTime = r.CookingTime,
                    ImageUrl = r.ImageUrl,
                    Category = r.Category.Name
                })
                .Take(4)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalRecipes = totalStatistics.TotalRecipes,
                TotalUsers = totalStatistics.TotalUsers,
                TotalCooks = totalStatistics.TotalCooks,
                Recipes = recipes
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
