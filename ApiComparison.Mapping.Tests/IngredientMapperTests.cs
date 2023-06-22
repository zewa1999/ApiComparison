using ApiComparison.Contracts.IngredientDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Xunit;

namespace ApiComparison.Mapping.Tests;

public class IngredientMapperTests
{
    [Fact]
    public void IngredientMapper_RequestToEntity_Pass()
    {
        // Arrange
        var requestDto = new IngredientRequestDto
        {
            Name = "Test Ingredient",
            Quantity = 1.0,
            UnitOfMeasure = "Test Unit"
        };
        var mapper = new IngredientMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.Equal(requestDto.Name, result.Name);
        Assert.Equal(requestDto.Quantity, result.Quantity);
        Assert.Equal(requestDto.UnitOfMeasure, result.UnitOfMeasure);
    }

    [Fact]
    public void IngredientMapper_RequestToEntity_Fail()
    {
        // Arrange
        var requestDto = new IngredientRequestDto
        {
            Name = "Test Ingredient",
            Quantity = 1.0,
            UnitOfMeasure = "Test Unit"
        };
        var mapper = new IngredientMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.NotEqual("WrongIngredient", result.Name);
    }

    [Fact]
    public void IngredientMapper_EntityToResponse_Pass()
    {
        // Arrange
        var ingredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Test Ingredient",
            Quantity = 1.0,
            UnitOfMeasure = "Test Unit"
        };
        var mapper = new IngredientMapper();

        // Act
        var result = mapper.EntityToResponse(ingredient);

        // Assert
        Assert.Equal(ingredient.Id, result.Id);
        Assert.Equal(ingredient.Name, result.Name);
        Assert.Equal(ingredient.Quantity, result.Quantity);
        Assert.Equal(ingredient.UnitOfMeasure, result.UnitOfMeasure);
    }

    [Fact]
    public void IngredientMapper_EntityToResponse_Fail()
    {
        // Arrange
        var ingredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Test Ingredient",
            Quantity = 1.0,
            UnitOfMeasure = "Test Unit"
        };
        var mapper = new IngredientMapper();

        // Act
        var result = mapper.EntityToResponse(ingredient);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
    }
}