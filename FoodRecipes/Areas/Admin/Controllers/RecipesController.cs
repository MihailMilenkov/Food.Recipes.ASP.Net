namespace FoodRecipes.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : AdminController
    {
        public IActionResult Index() 
            => View();
    }
}
