namespace FoodRecipes.Services.Ingredients
{
    using System.Collections.Generic;
    using System.Linq;
    using FoodRecipes.Data;
    using FoodRecipes.Data.Models;
    using FoodRecipes.Models;

    public class IngredientService : IIngredientService
    {
        private readonly FoodRecipesDbContext data;

        public IngredientService(FoodRecipesDbContext data)
            => this.data = data;

        public IngredientQueryServiceModel All(
            string name,
            string searchTerm,
            IngredientSorting sorting,
            int currentPage,
            int ingredientsPerPage)
        {
            var ingredientsQuery = this.data.Ingredients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                ingredientsQuery = ingredientsQuery.
                    Where(i => i.Name == name);
            }


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                ingredientsQuery = ingredientsQuery.Where(r =>
                    r.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    r.Category.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            ingredientsQuery = sorting switch
            {
                IngredientSorting.Name => ingredientsQuery.OrderBy(r => r.Name),
                IngredientSorting.Category => ingredientsQuery.OrderBy(r => r.Category.Name),
                IngredientSorting.DateCreated or _ => ingredientsQuery.OrderByDescending(r => r.Id)
            };

            var totalIngredients = ingredientsQuery.Count();

            var ingredients = GetIngredients(ingredientsQuery
                                      .Skip((currentPage - 1) * ingredientsPerPage))
                                      .Take(ingredientsPerPage);

            return new IngredientQueryServiceModel
            {
                CurrentPage = currentPage,
                IngredientsPerPage = ingredientsPerPage,
                TotalIngredients = totalIngredients,
                Ingredients = ingredients,
            };
        }

        public IngredientDetailsServiceModel Details(int id)
            => this.data
                .Ingredients
                .Where(i => i.Id == id)
                .Select(i => new IngredientDetailsServiceModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    CategoryName = i.Category.Name,
                    ImageUrl = i.ImageUrl,
                    UserId = i.Seller.UserId,
                    SellerId = i.SellerId,
                    SellerName = i.Seller.Name
                })
                .FirstOrDefault();

        public int Create(
            string name,
            string imageUrl,
            int categoryId,
            int sellerId)
        {
            var ingredientData = new Ingredient
            {
                Name = name,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                SellerId = sellerId
            };

            this.data.Ingredients.Add(ingredientData);

            this.data.SaveChanges();

            return ingredientData.Id;
        }

        public bool Edit(
            int id,
            string name,
            string imageUrl,
            int categoryId)
        {
            var ingredientData = this.data.Ingredients.Find(id);

            if (ingredientData == null)
            {
                return false;
            }

            ingredientData.Name = name;
            ingredientData.ImageUrl = imageUrl;
            ingredientData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<IngredientServiceModel> GetIngredientsByUser(string userId)
            => GetIngredients(this.data
                .Ingredients
                .Where(i => i.Seller.UserId == userId));

        public bool IngredientIsFromSeller(int ingredientId, int sellerId)
            => this.data
                .Ingredients
                .Any(i => i.Id == ingredientId && i.SellerId == sellerId);

        public IEnumerable<string> AllIngredientNames()
            => this.data
                .Ingredients
                .Select(r => r.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

        public IEnumerable<IngredientCategoryServiceModel> AllIngredientCategories()
            => this.data
                .IngredientCategories
                .Select(c => new IngredientCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public bool CategoryExists(int categoryId)
            => this.data
                .IngredientCategories
                .Any(ic => ic.Id == categoryId);

        private static IEnumerable<IngredientServiceModel> GetIngredients(IQueryable<Ingredient> ingredientQuery)
            => ingredientQuery
                .Select(r => new IngredientServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CategoryName = r.Category.Name,
                    ImageUrl = r.ImageUrl,
                })
                .ToList();
    }
}

