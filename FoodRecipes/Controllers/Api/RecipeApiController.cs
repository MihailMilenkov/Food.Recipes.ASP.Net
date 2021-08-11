namespace FoodRecipes.Controllers.Api
{
    using System.Collections;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Data;
    using FoodRecipes.Models.Recipes;

    [ApiController]
    [Route("api/recipes")]
    public class RecipeApiController : ControllerBase
    {
        private readonly FoodRecipesDbContext data;

        public RecipeApiController(FoodRecipesDbContext data)
            => this.data = data;

        [HttpGet]
        public IEnumerable GetRecipe()
        {
            return this.data.Recipes.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public object Details(int recipeId)
        {
            return this.data.Recipes.Find(recipeId);
            var recipe = this.data.Recipes.Find(recipeId);

            return new RecipeDetalsServiceModel
            {
                Name = recipe.Name,
                Ingredients = recipe.Ingredients,
                Directions = recipe.Directions,
                ImageUrl = recipe.ImageUrl,
                Category = recipe.Category.Name,
                CookingTime = recipe.CookingTime
            };
        }
    }
}
