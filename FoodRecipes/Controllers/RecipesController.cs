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

    public class RecipesController : Controller
    {
        private readonly FoodRecipesDbContext data;

        public RecipesController(FoodRecipesDbContext data)
            => this.data = data;

        //public IActionResult Details(int recepiId) // Deprecated method
        //{
        //    var recipe = this.data
        //        .Recipes
        //        .Where(r => r.Id == recepiId)
        //        .FirstOrDefault();

        //    if (recipe == null)
        //    {
        //        return View(new RecipeDetalsServiceModel
        //        {
        //            Name = "invalid",
        //            CookingTime = 0,
        //        });
        //    }

        //    var recipeToShow = new RecipeDetalsServiceModel
        //    {
        //        Name = recipe.Name,
        //        Ingredients = recipe.Ingredients,
        //        Directions = recipe.Directions,
        //        ImageUrl = recipe.ImageUrl,
        //        Category = recipe.Category.Name,
        //        CookingTime = recipe.CookingTime
        //    };

        //    return View(recipeToShow);
        //}

        // HTTPGet
        public IActionResult All([FromQuery] AllRecipesQueryModel query)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                recipesQuery = recipesQuery.
                    Where(r => r.Name == query.Name);
            }


            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    r.Ingredients.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    r.Category.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    r.Directions.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            recipesQuery = query.Sorting switch
            {
                RecipeSorting.Name => recipesQuery.OrderBy(r => r.Name),
                RecipeSorting.Category => recipesQuery.OrderBy(r => r.Category.Name),
                RecipeSorting.CookingTime => recipesQuery.OrderBy(r => r.CookingTime),
                RecipeSorting.DateCreated or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .Skip((query.CurrentPage - 1) * AllRecipesQueryModel.RecipesPerPage)
                .Take(AllRecipesQueryModel.RecipesPerPage)
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CookingTime = r.CookingTime,
                    Category = r.Category.Name,
                    ImageUrl = r.ImageUrl,
                })
                .ToList();

            var recipeNames = this.data
                .Recipes
                .Select(r => r.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            query.TotalRecipes = totalRecipes;
            query.Names = recipeNames;
            query.Recipes = recipes;

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
