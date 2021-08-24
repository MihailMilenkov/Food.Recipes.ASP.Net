namespace FoodRecipes.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using FoodRecipes.Services.Recipes;
    using FoodRecipes.Infrastructure;
    using FoodRecipes.Services.Cooks;
    using FoodRecipes.Models.Recipes;
    using AutoMapper;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipes;
        private readonly ICookService cooks;
        private readonly IMapper mapper;

        public RecipesController(
            IRecipeService recipes,
            ICookService cooks,
            IMapper mapper)
        {
            this.recipes = recipes;
            this.cooks = cooks;
            this.mapper = mapper;
        }

        // HTTPGet
        public IActionResult All([FromQuery] AllRecipesQueryModel query)
        {
            var queryResult = this.recipes.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllRecipesQueryModel.RecipesPerPage);

            var recipeNames = this.recipes.AllRecipeNames();

            query.Names = recipeNames;
            query.TotalRecipes = queryResult.TotalRecipes;
            query.Recipes = queryResult.Recipes;

            return View(query);
        }

        [Authorize]
        public IActionResult MyRecipes()
        {
            var myRecieps = this.recipes.GetRecipesByUser(this.User.GetId());

            return View(myRecieps);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.cooks.IsCook(this.User.GetId()))
            {
                return RedirectToAction(nameof(CooksController.Become), "Cooks");
            }

            return View(new RecipeFormModel
            {
                Categories = this.recipes.AllRecipeCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(RecipeFormModel recipe)
        {
            var cookId = this.cooks.GetCookIdByUserId(this.User.GetId());

            if (cookId == 0)
            {
                return RedirectToAction(nameof(CooksController.Become), "Cooks");
            }

            if (!this.recipes.CategoryExists(recipe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.CategoryId), "Category does not exists.");
            }

            if (!ModelState.IsValid)
            {
                recipe.Categories = this.recipes.AllRecipeCategories();

                return View(recipe);
            }

            this.recipes.Create(
                recipe.Name,
                recipe.Ingredients,
                recipe.Directions,
                recipe.ImageUrl,
                recipe.CookingTime,
                recipe.CategoryId,
                cookId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.cooks.IsCook(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(CooksController.Become), "Cooks");
            }

            var recipe = this.recipes.Details(id);

            if (recipe.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var recipeForm = this.mapper.Map<RecipeFormModel>(recipe);

            recipeForm.Categories = this.recipes.AllRecipeCategories();

            return View(recipeForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, RecipeFormModel recipe)
        {
            var cookId = this.cooks.GetCookIdByUserId(this.User.GetId());

            if (cookId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(CooksController.Become), "Cooks");
            }

            if (!this.recipes.CategoryExists(recipe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.CategoryId), "Category does not exists.");
            }

            if (!ModelState.IsValid)
            {
                recipe.Categories = this.recipes.AllRecipeCategories();

                return View(recipe);
            }

            if (!this.recipes.RecipeIsFromCook(id, cookId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.recipes.Edit(
                id,
                recipe.Name,
                recipe.Ingredients,
                recipe.Directions,
                recipe.ImageUrl,
                recipe.CookingTime,
                recipe.CategoryId);

            return RedirectToAction(nameof(All));
        }


        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var recipe = this.recipes.Details(id);

            return View(recipe);
        }
    }
}
