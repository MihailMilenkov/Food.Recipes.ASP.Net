namespace FoodRecipes.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using FoodRecipes.Data.Models;

    public class FoodRecipesDbContext : IdentityDbContext
    {
        public FoodRecipesDbContext(DbContextOptions<FoodRecipesDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Category> Cooks { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<Recipe>()
            //    .HasOne(r => r.Cook)
            //    .WithMany(c => c.Recipes)
            //    .HasForeignKey(r => r.CookId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .Entity<Cook>()
            //    .HasOne<User>()
            //    .WithOne()
            //    .HasForeignKey<Cook>(c => c.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
