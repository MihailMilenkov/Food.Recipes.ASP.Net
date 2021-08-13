namespace FoodRecipes.Controllers.Api
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Data.Models;
    using FoodRecipes.Models.Api.Recipes;
    using FoodRecipes.Services.Recipes;

    [ApiController]
    [Route("api/recipes")]
    public class RecipesApiController : ControllerBase
    {
        private readonly IRecipeService recipes;

        public RecipesApiController(IRecipeService recipes)
            => this.recipes = recipes;

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDetails(int recipeId) // WIP
        {
            throw new NotImplementedException();
            //var recipe = GetRecipe(recipeId);

            //if (recipe == null)
            //{
            //    return NotFound();
            //}

            //return Ok(recipe); // WIP, this method cant get parameters, id is always 0
        }

        public ActionResult<Recipe> GetRecipe(int id) // WIP
        {
            //return this.data.Recipes.Find(id); // problems with ids in GetDetails

            throw new NotImplementedException();
        }

        [HttpGet]
        public RecipeQueryServiceModel All([FromQuery] AllRecipesApiRequestModel query)
            => this.recipes.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.RecipesPerPage);
    }
}
