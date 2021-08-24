namespace FoodRecipes.Test.Controllers.Api
{
    using FoodRecipes.Controllers.Api;
    using FoodRecipes.Test.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalCooks);
            Assert.Equal(10, result.TotalRecipes);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
