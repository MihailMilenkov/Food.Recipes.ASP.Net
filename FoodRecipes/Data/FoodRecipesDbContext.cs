namespace FoodRecipes.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using FoodRecipes.Data.Models;

    public class FoodRecipesDbContext : IdentityDbContext<User>
    {
        public FoodRecipesDbContext(DbContextOptions<FoodRecipesDbContext> options)
            : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<RecipeCategory> RecipeCategories { get; init; }

        public DbSet<Cook> Cooks { get; init; }

        public DbSet<Ingredient> Ingredients { get; init; }

        public DbSet<IngredientCategory> IngredientCategories { get; init; }

        public DbSet<Seller> Sellers { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Recipe>()
                .HasOne(r => r.Cook)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Cook>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Cook>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Ingredient>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Ingredients)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Ingredient>()
                .HasOne(i => i.Seller)
                .WithMany(s => s.Ingredients)
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Seller>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Seller>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(builder);
        }
    }
}
