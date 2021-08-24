namespace FoodRecipes.Infrastructure
{
    using AutoMapper;
    using FoodRecipes.Data.Models;
    using FoodRecipes.Models.Recipes;
    using FoodRecipes.Models.Home;
    using FoodRecipes.Services.Recipes.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Recipe, LatestRecipeServiceModel>();

            this.CreateMap<RecipeDetailsServiceModel, RecipeFormModel>();

            this.CreateMap<Recipe, RecipeDetailsServiceModel>()
                .ForMember(r => r.UserId, cfg => cfg.MapFrom(r => r.Cook.UserId));
        }
    }
}
