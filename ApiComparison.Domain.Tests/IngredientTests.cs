using ApiComparison.Domain.Entities;
using Xunit;

namespace ApiComparison.Domain.Tests;

public class IngredientTests
{

    [Fact]
    public void Ingredient_Properties_Pass()
    {
        // Arrange
        var ingredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "Test Ingredient",
            Quantity = 1.5,
            UnitOfMeasure = "Test Unit",
            Dishes = new List<Dish>()
        };

        // Assert
        Assert.NotEqual(Guid.Empty, ingredient.Id);
        Assert.NotEqual(default(DateTime), ingredient.CreatedAt);
        Assert.NotEqual(default(DateTime), ingredient.LastUpdatedAt);
        Assert.Equal("Test Ingredient", ingredient.Name);
        Assert.Equal(1.5, ingredient.Quantity);
        Assert.Equal("Test Unit", ingredient.UnitOfMeasure);
        Assert.NotNull(ingredient.Dishes);
    }

    [Fact]
    public void Ingredient_Properties_Fail()
    {
        // Arrange
        var ingredient = new Ingredient
        {
            Id = Guid.Empty,
            CreatedAt = default(DateTime),
            LastUpdatedAt = default(DateTime),
            Name = "Test Ingredient",
            Quantity = 1.5,
            UnitOfMeasure = "Test Unit",
            Dishes = new List<Dish>()
        };

        // Assert
        Assert.Equal(Guid.Empty, ingredient.Id);
        Assert.Equal(default(DateTime), ingredient.CreatedAt);
        Assert.Equal(default(DateTime), ingredient.LastUpdatedAt);
        Assert.NotEqual("Wrong Ingredient", ingredient.Name);
        Assert.NotEqual(2.0, ingredient.Quantity);
        Assert.NotEqual("Wrong Unit", ingredient.UnitOfMeasure);
        Assert.NotNull(ingredient.Dishes);
    }

}
