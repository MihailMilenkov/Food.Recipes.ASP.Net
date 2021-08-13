namespace FoodRecipes.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using FoodRecipes.Models.Recipes;
    using FoodRecipes.Data;
    using FoodRecipes.Data.Models;
    using FoodRecipes.Infrastructure;
    using FoodRecipes.Services.Recipes;

    public class RecipesController : Controller
    {
        private readonly IRecipeService recipes;
        private readonly FoodRecipesDbContext data;

        public RecipesController(IRecipeService recipes, FoodRecipesDbContext data)
        {
            this.recipes = recipes;
            this.data = data;
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

            var recipeNames = this.recipes.AllRecipesNames();

            query.Names = recipeNames;
            query.TotalRecipes = queryResult.TotalRecipes;
            query.Recipes = queryResult.Recipes;

            return View(query);
        }

        // HTTPGet
        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsCook())
            {
                return RedirectToAction(nameof(CooksController.Become), "Cooks");
            }

            return View(new AddRecipeFormModel
            {
                Categories = this.GetRecipeCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            var cookId = this.data
                .Cooks
                .Where(c => c.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (cookId == 0)
            {
                return RedirectToAction(nameof(CooksController.Become), "Cooks");
            }

            if (!this.data.Categories.Any(c => c.Id == recipe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.CategoryId), "Category does not exists.");
            }

            if (!ModelState.IsValid)
            {
                recipe.Categories = this.GetRecipeCategories();

                return View(recipe);
            }

            var recipeData = new Recipe
            {
                Name = recipe.Name,
                Ingredients = recipe.Ingredients,
                Directions = recipe.Directions,
                ImageUrl = recipe.ImageUrl,
                CookingTime = recipe.CookingTime,
                CategoryId = recipe.CategoryId,
                CookId = cookId
            };

            this.data.Recipes.Add(recipeData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsCook()
            => this.data
                .Cooks
                .Any(c => c.UserId == this.User.GetId());

        private IEnumerable<RecipeCategoryViewModel> GetRecipeCategories()
            => this.data
                .Categories
                .Select(c => new RecipeCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
            .ToList();
    }
}
