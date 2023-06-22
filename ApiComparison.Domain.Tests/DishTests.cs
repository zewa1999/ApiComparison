using ApiComparison.Domain.Entities;
using Xunit;

namespace ApiComparison.Domain.Tests;

public class DishTests
{

    [Fact]
    public void Dish_Properties_Pass()
    {
        // Arrange
        var dish = new Dish
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "testurl.com",
            Ingredients = new List<Ingredient>()
        };

        // Assert
        Assert.NotEqual(Guid.Empty, dish.Id);
        Assert.NotEqual(default(DateTime), dish.CreatedAt);
        Assert.NotEqual(default(DateTime), dish.LastUpdatedAt);
        Assert.Equal("Test Dish", dish.Name);
        Assert.Equal("Test Description", dish.Description);
        Assert.Equal("testurl.com", dish.PhotoUrl);
        Assert.NotNull(dish.Ingredients);
    }

    [Fact]
    public void Dish_Properties_Fail()
    {
        // Arrange
        var dish = new Dish
        {
            Id = Guid.Empty,
            CreatedAt = default(DateTime),
            LastUpdatedAt = default(DateTime),
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "testurl.com",
            Ingredients = new List<Ingredient>()
        };

        // Assert
        Assert.Equal(Guid.Empty, dish.Id);
        Assert.Equal(default(DateTime), dish.CreatedAt);
        Assert.Equal(default(DateTime), dish.LastUpdatedAt);
        Assert.NotEqual("Wrong Dish", dish.Name);
        Assert.NotEqual("Wrong Description", dish.Description);
        Assert.NotEqual("wrongurl.com", dish.PhotoUrl);
        Assert.NotNull(dish.Ingredients);
    }

}
