namespace FoodRecipes.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using FoodRecipes.Services.Ingredients;
    using FoodRecipes.Infrastructure;
    using FoodRecipes.Models.Ingredients;
    using FoodRecipes.Services.Sellers;

    public class IngredientsController : Controller
    {
        private readonly IIngredientService ingredients;
        private readonly ISellerService sellers;

        public IngredientsController(
            IIngredientService ingredients,
            ISellerService sellers)
        {
            this.ingredients = ingredients;
            this.sellers = sellers;
        }

        // HTTPGet
        public IActionResult All([FromQuery] AllIngredientsQueryModel query)
        {
            var queryResult = this.ingredients.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllIngredientsQueryModel.IngredientsPerPage);

            var ingredientNames = this.ingredients.AllIngredientNames();

            query.Names = ingredientNames;
            query.TotalRecipes = queryResult.TotalIngredients;
            query.Ingredients = queryResult.Ingredients;

            return View(query);
        }

        [Authorize]
        public IActionResult MyIngredients()
        {
            var myIngredients = this.ingredients.GetIngredientsByUser(this.User.GetId());

            return View(myIngredients);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.sellers.IsSeller(this.User.GetId()))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            return View(new IngredientFormModel
            {
                Categories = this.ingredients.AllIngredientCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(IngredientFormModel ingredient)
        {
            var sellerId = this.sellers.GetSellerIdByUserId(this.User.GetId());

            if (sellerId == 0)
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.ingredients.CategoryExists(ingredient.CategoryId))
            {
                this.ModelState.AddModelError(nameof(ingredient.CategoryId), "Category does not exists.");
            }

            if (!ModelState.IsValid)
            {
                ingredient.Categories = this.ingredients.AllIngredientCategories();

                return View(ingredient);
            }

            this.ingredients.Create(
                ingredient.Name,
                ingredient.ImageUrl,
                ingredient.CategoryId,
                sellerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.sellers.IsSeller(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            var ingredient = this.ingredients.Details(id);

            if (ingredient.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new IngredientFormModel
            {
                Name = ingredient.Name,
                ImageUrl = ingredient.ImageUrl,
                CategoryId = ingredient.CategoryId,
                Categories = this.ingredients.AllIngredientCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, IngredientFormModel ingredient)
        {
            var sellerId = this.sellers.GetSellerIdByUserId(this.User.GetId());

            if (sellerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            if (!this.ingredients.CategoryExists(ingredient.CategoryId))
            {
                this.ModelState.AddModelError(nameof(ingredient.CategoryId), "Category does not exists.");
            }

            if (!ModelState.IsValid)
            {
                ingredient.Categories = this.ingredients.AllIngredientCategories();

                return View(ingredient);
            }

            if (!this.ingredients.IngredientIsFromSeller(id, sellerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.ingredients.Edit(
                id,
                ingredient.Name,
                ingredient.ImageUrl,
                ingredient.CategoryId);

            return RedirectToAction(nameof(All));
        }


        [Route("{id}/ingredients")]
        public IActionResult Details(int id)
        {
            var ingredient = this.ingredients.Details(id);

            return View(ingredient);
        }
    }
}
