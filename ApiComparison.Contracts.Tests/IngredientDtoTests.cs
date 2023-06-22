using ApiComparison.Contracts.IngredientDtos;
using ApiComparison.Contracts.UserDtos;
using Xunit;

namespace ApiComparison.Contracts.Tests;

public class IngredientDtoTests
{
    [Fact]
    public void IngredientRequestDto_Creation_Success()
    {
        var ingredientRequestDto = new IngredientRequestDto() { Name = "Test Ingredient", Quantity = 1.0, UnitOfMeasure = "Test Unit" };

        Assert.Equal("Test Ingredient", ingredientRequestDto.Name);
        Assert.Equal(1.0, ingredientRequestDto.Quantity);
        Assert.Equal("Test Unit", ingredientRequestDto.UnitOfMeasure);
    }

    [Fact]
    public void IngredientRequestDto_Creation_Failure()
    {
        var ingredientRequestDto = new IngredientRequestDto() { Name = "Test Ingredient", Quantity = 1.0, UnitOfMeasure = "Test Unit" };

        Assert.NotEqual("WrongIngredient", ingredientRequestDto.Name);
    }

    [Fact]
    public void IngredientResponseDto_Creation_Success()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var ingredientResponseDto = new IngredientResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, Name = "Test Ingredient", Quantity = 1.0, UnitOfMeasure = "Test Unit" };

        Assert.Equal(id, ingredientResponseDto.Id);
        Assert.Equal(now, ingredientResponseDto.CreatedAt);
        Assert.Equal(now, ingredientResponseDto.LastUpdatedAt);
        Assert.Equal("Test Ingredient", ingredientResponseDto.Name);
        Assert.Equal(1.0, ingredientResponseDto.Quantity);
        Assert.Equal("Test Unit", ingredientResponseDto.UnitOfMeasure);
    }

    [Fact]
    public void IngredientResponseDto_Creation_Failure()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var ingredientResponseDto = new IngredientResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, Name = "Test Ingredient", Quantity = 1.0, UnitOfMeasure = "Test Unit" };

        Assert.NotEqual("WrongIngredient", ingredientResponseDto.Name);
    }


}
