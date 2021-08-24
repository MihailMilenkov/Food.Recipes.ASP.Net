namespace FoodRecipes.Test.Mocks
{
    using FoodRecipes.Services.Statistics;
    using Moq;
    using System;
    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalUsers = 15,
                        TotalCooks = 5,
                        TotalRecipes = 10
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
