namespace FoodRecipes.Test.Mocks
{
    using System;
    using FoodRecipes.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseMock
    {
        public static FoodRecipesDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<FoodRecipesDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new FoodRecipesDbContext(dbContextOptions);
            }
        }
    }
}
