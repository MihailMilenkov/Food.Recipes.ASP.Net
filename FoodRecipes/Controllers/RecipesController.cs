namespace FoodRecipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Models.Recipes;
    using FoodRecipes.Data;
    using FoodRecipes.Data.Models;

    public class RecipesController : Controller
    {
        private readonly FoodRecipesDbContext data;

        public RecipesController(FoodRecipesDbContext data)
            => this.data = data;

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
        public IActionResult Add() => View(new AddRecipeFormModel
        {
            Categories = this.GetRecipeCategories()
        });

        [HttpPost]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
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
                CategoryId = recipe.CategoryId
            };

            this.data.Recipes.Add(recipeData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

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
