namespace FoodRecipes.Services.Cooks
{
    public interface ICookService
    {
        public bool IsCook(string userId);

        public int GetCookIdByUserId(string userId);
    }
}
