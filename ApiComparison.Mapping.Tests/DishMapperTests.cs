using ApiComparison.Contracts.DishDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Xunit;

namespace ApiComparison.Mapping.Tests;

public class DishMapperTests
{
    [Fact]
    public void DishMapper_RequestToEntity_Pass()
    {
        // Arrange
        var requestDto = new DishRequestDto
        {
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "testurl.com",
            IngredientsIds = new HashSet<Guid> { Guid.NewGuid() }
        };
        var mapper = new DishMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.Equal(requestDto.Name, result.Name);
        Assert.Equal(requestDto.Description, result.Description);
        Assert.Equal(requestDto.PhotoUrl, result.PhotoUrl);
    }

    [Fact]
    public void DishMapper_RequestToEntity_Fail()
    {
        // Arrange
        var requestDto = new DishRequestDto
        {
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "testurl.com",
            IngredientsIds = new HashSet<Guid> { Guid.NewGuid() }
        };
        var mapper = new DishMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.NotEqual("WrongDish", result.Name);
    }

    // DishMapper - EntityToResponse
    [Fact]
    public void DishMapper_EntityToResponse_Pass()
    {
        // Arrange
        var dish = new Dish
        {
            Id = Guid.NewGuid(),
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "testurl.com"
        };
        var mapper = new DishMapper();

        // Act
        var result = mapper.EntityToResponse(dish);

        // Assert
        Assert.Equal(dish.Id, result.Id);
        Assert.Equal(dish.Name, result.Name);
        Assert.Equal(dish.Description, result.Description);
        Assert.Equal(dish.PhotoUrl, result.PhotoUrl);
    }

    [Fact]
    public void DishMapper_EntityToResponse_Fail()
    {
        // Arrange
        var dish = new Dish
        {
            Id = Guid.NewGuid(),
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "testurl.com"
        };
        var mapper = new DishMapper();

        // Act
        var result = mapper.EntityToResponse(dish);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
    }

}
