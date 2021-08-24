namespace FoodRecipes.Services.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FoodRecipes.Data;
    using FoodRecipes.Data.Models;
    using FoodRecipes.Models;
    using FoodRecipes.Services.Recipes.Models;

    public class RecipeService : IRecipeService
    {
        private readonly FoodRecipesDbContext data;
        private readonly IConfigurationProvider mapper;

        public RecipeService(FoodRecipesDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public RecipeQueryServiceModel All(
            string name,
            string searchTerm,
            RecipeSorting sorting,
            int currentPage,
            int recipesPerPage)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                recipesQuery = recipesQuery.
                    Where(r => r.Name == name);
            }


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Ingredients.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Category.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Directions.ToLower().Contains(searchTerm.ToLower()));
            }

            recipesQuery = sorting switch
            {
                RecipeSorting.Name => recipesQuery.OrderBy(r => r.Name),
                RecipeSorting.Category => recipesQuery.OrderBy(r => r.Category.Name),
                RecipeSorting.CookingTime => recipesQuery.OrderBy(r => r.CookingTime),
                RecipeSorting.DateCreated or _ => recipesQuery.OrderByDescending(r => r.Id)
            };

            var totalRecipes = recipesQuery.Count();

            var recipes = GetRecipes(recipesQuery
                                      .Skip((currentPage - 1) * recipesPerPage))
                                      .Take(recipesPerPage);

            return new RecipeQueryServiceModel
            {
                CurrentPage = currentPage,
                RecipesPerPage = recipesPerPage,
                TotalRecipes = totalRecipes,
                Recipes = recipes,
            };
        }

        public IEnumerable<LatestRecipeServiceModel> Latest()
            => this.data
                .Recipes
                .OrderByDescending(r => r.Id)
                .ProjectTo<LatestRecipeServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public RecipeDetailsServiceModel Details(int id)
            => this.data
                .Recipes
                .Where(r => r.Id == id)
                .ProjectTo<RecipeDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public int Create(
            string name,
            string ingredients,
            string directions,
            string imageUrl,
            int cookingTime,
            int categoryId,
            int cookId)
        {
            var recipeData = new Recipe
            {
                Name = name,
                Ingredients = ingredients,
                Directions = directions,
                ImageUrl = imageUrl,
                CookingTime = cookingTime,
                CategoryId = categoryId,
                CookId = cookId
            };

            this.data.Recipes.Add(recipeData);

            this.data.SaveChanges();

            return recipeData.Id;
        }

        public bool Edit(
            int id,
            string name,
            string ingredients,
            string directions,
            string imageUrl,
            int cookingTime,
            int categoryId)
        {
            var recipeData = this.data.Recipes.Find(id);

            if (recipeData==null)
            {
                return false;
            }

            recipeData.Name = name;
            recipeData.Ingredients = ingredients;
            recipeData.Directions = directions;
            recipeData.ImageUrl = imageUrl;
            recipeData.CookingTime = cookingTime;
            recipeData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<RecipeServiceModel> GetRecipesByUser(string userId)
            => GetRecipes(this.data
                .Recipes
                .Where(r => r.Cook.UserId == userId));

        public bool RecipeIsFromCook(int recipeId, int cookId)
            => this.data
                .Recipes
                .Any(r => r.Id == recipeId && r.CookId == cookId);

        public IEnumerable<string> AllRecipeNames()
            => this.data
                .Recipes
                .Select(r => r.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        public IEnumerable<RecipeCategoryServiceModel> AllRecipeCategories()
            => this.data
                .RecipeCategories
                .Select(c => new RecipeCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public bool CategoryExists(int categoryId)
            => this.data
                .RecipeCategories
                .Any(rc => rc.Id == categoryId);

        private static IEnumerable<RecipeServiceModel> GetRecipes(IQueryable<Recipe> recipeQuery)
            => recipeQuery
                .Select(r => new RecipeServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CookingTime = r.CookingTime,
                    CategoryName = r.Category.Name,
                    ImageUrl = r.ImageUrl,
                })
                .ToList();
    }
}

