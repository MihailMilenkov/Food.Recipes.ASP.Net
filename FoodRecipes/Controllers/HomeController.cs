namespace FoodRecipes.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Models;
    using FoodRecipes.Data;
    using FoodRecipes.Models.Home;

    public class HomeController : Controller
    {
        private readonly FoodRecipesDbContext data;

        public HomeController(FoodRecipesDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalRecipes = this.data.Recipes.Count();
            var totalUsers = this.data.Users.Count();
            var totalCooks = this.data.Cooks.Count();

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

            return View(new IndexViewModel
            {
                TotalRecipes = totalRecipes,
                TotalUsers = totalUsers,
                TotalCooks = totalCooks,
                Recipes = recipes
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
