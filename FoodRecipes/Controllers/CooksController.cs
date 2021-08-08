namespace FoodRecipes.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Data;
    using FoodRecipes.Models.Cooks;
    using FoodRecipes.Infrastructure;
    using FoodRecipes.Data.Models;

    public class CooksController : Controller
    {
        private readonly FoodRecipesDbContext data;

        public CooksController(FoodRecipesDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeCookFormModel cook)
        {
            var userId = this.User.GetId();

            var userIsAlreadyCook = this.data
                .Cooks
                .Any(c => c.UserId == userId);

            if (userIsAlreadyCook)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(cook);
            }

            var cookData = new Cook
            {
                Name = cook.Name,
                PhoneNumber = cook.PhoneNumber,
                UserId = userId,
            };

            this.data.Cooks.Add(cookData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Recipes");
        }
    }
}
