namespace FoodRecipes.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Services;
    using FoodRecipes.Data;
    using FoodRecipes.Models.Home;
    using FoodRecipes.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly FoodRecipesDbContext data;
        private readonly IConfigurationProvider mapper;

        public HomeController(IStatisticsService statistics, FoodRecipesDbContext data, IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IActionResult Index()
        {
            var recipes = this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .ProjectTo<RecipeIndexViewModel>(this.mapper)
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
