namespace FoodRecipes.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area(AdminConstants.AreaName)]
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
