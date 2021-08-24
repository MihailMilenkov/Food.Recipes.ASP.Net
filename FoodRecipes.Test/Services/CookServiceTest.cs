namespace FoodRecipes.Test.Services
{
    using FoodRecipes.Data.Models;
    using FoodRecipes.Services.Cooks;
    using FoodRecipes.Test.Mocks;
    using Xunit;

    public class CookServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsCookShouldReturnTrueWhenUserIsCook()
        {
            // Arrange
            var cookService = GetCookService();

            // Act
            var result = cookService.IsCook(UserId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCookShouldReturnFalseWhenUSerIsNotCook()
        {
            // Arrange
            var cookService = GetCookService();

            // Act
            var result = cookService.IsCook("AnotherUserId");

            // Assert
            Assert.False(result);
        }

        private static ICookService GetCookService()
        {
            var data = DatabaseMock.Instance;

            data.Cooks.Add(new Cook { UserId = UserId });
            data.SaveChanges();

            return new CookService(data);
        }
    }
}
