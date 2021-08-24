namespace FoodRecipes.Test.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using FoodRecipes.Controllers;
    using FoodRecipes.Data.Models;
    using FoodRecipes.Models.Home;
    using FoodRecipes.Services.Recipes;
    using FoodRecipes.Services.Statistics;
    using FoodRecipes.Test.Mocks;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModelAndData()
        //    => MyMvc
        //        .Pipeline()
        //        .ShouldMap("/")
        //        .To<HomeController>(c => c.Index())
        //        .Which(controller => controller
        //            .WithData(GetRecipes()))
        //        .ShouldReturn()
        //        .View(view => view
        //            .WithModelOfType<IndexViewModel>()
        //            .Passing(m => m.Recipes.Should().HaveCount(3)));

        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModel()
        //{
        //    // Arrange
        //    var data = DatabaseMock.Instance;
        //    var mapper = MapperMock.Instance;

        //    var recipes = Enumerable
        //        .Range(0, 10)
        //        .Select(i => new Recipe());

        //    data.Recipes.AddRange(recipes);
        //    data.Users.Add(new User());

        //    data.SaveChanges();

        //    var recipeService = new RecipeService(data, mapper);
        //    var statisticsService = new StatisticsService(data);

        //    var homeController = new HomeController(recipeService, statisticsService);

        //    // Act
        //    var result = homeController.Index();

        //    // Assert
        //    // Assert.NotNull(result);

        //    // var viewResult = Assert.IsType<ViewResult>(result);

        //    // var model = viewResult.Model;

        //    // var indexViewModel = Assert.IsType<IndexViewModel>(model);

        //    // Assert.Equal(3, indexViewModel.Recipes.Count);
        //    // Assert.Equal(10, indexViewModel.TotalCars);
        //    // Assert.Equal(1, indexViewModel.TotalUsers);

        //    result
        //        .Should()
        //        .NotBeNull()
        //        .And
        //        .BeAssignableTo<ViewResult>()
        //        .Which
        //        .Model
        //        .As<IndexViewModel>()
        //        .Invoking(model =>
        //        {
        //            model.Recipes.Should().HaveCount(3);
        //            model.TotalRecipes.Should().Be(10);
        //            model.TotalUsers.Should().Be(1);
        //        })
        //        .Invoke();
        //}

        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(
                null,
                null);

            // Act
            var result = homeController.Error;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        private static IEnumerable<Recipe> GetRecipes()
            => Enumerable.Range(0, 10).Select(i => new Recipe());
    }
}
