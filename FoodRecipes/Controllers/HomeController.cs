namespace FoodRecipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using FoodRecipes.Services.Recipes;
    using FoodRecipes.Services.Recipes.Models;

    public class HomeController : Controller
    {
        private readonly IRecipeService recipes;
        private readonly IMemoryCache cache;

        public HomeController(IRecipeService recipes, IMemoryCache cache)
        {
            this.recipes = recipes;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestRecipesCacheKey = "LatestRecipesCacheKey";

            var latestRecipes = this.cache.Get<List<LatestRecipeServiceModel>>(latestRecipesCacheKey);

            if (latestRecipes == null)
            {
                latestRecipes = this.recipes
                   .Latest()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestRecipesCacheKey, latestRecipes, cacheOptions);
            }

            return View(latestRecipes);
        }

        public IActionResult Error => View();

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
