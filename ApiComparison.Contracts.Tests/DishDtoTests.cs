using ApiComparison.Contracts.DishDtos;
using Xunit;

namespace ApiComparison.Contracts.Tests;

public class DishDtoTests
{
    [Fact]
    public void DishRequestDto_Creation_Success()
    {
        var dishRequestDto = new DishRequestDto() { Name = "Test Dish", Description = "Test Description", PhotoUrl = "testurl.com", IngredientsIds = new HashSet<Guid>() { Guid.NewGuid() } };

        Assert.Equal("Test Dish", dishRequestDto.Name);
        Assert.Equal("Test Description", dishRequestDto.Description);
        Assert.Equal("testurl.com", dishRequestDto.PhotoUrl);
        Assert.Single(dishRequestDto.IngredientsIds);
    }

    [Fact]
    public void DishRequestDto_Creation_Failure()
    {
        var dishRequestDto = new DishRequestDto() { Name = "Test Dish", Description = "Test Description", PhotoUrl = "testurl.com", IngredientsIds = new HashSet<Guid>() { Guid.NewGuid() } };

        Assert.NotEqual("WrongDish", dishRequestDto.Name);
    }

    [Fact]
    public void DishResponseDto_Creation_Success()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var dishResponseDto = new DishResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, Name = "Test Dish", Description = "Test Description", PhotoUrl = "testurl.com" };

        Assert.Equal(id, dishResponseDto.Id);
        Assert.Equal(now, dishResponseDto.CreatedAt);
        Assert.Equal(now, dishResponseDto.LastUpdatedAt);
        Assert.Equal("Test Dish", dishResponseDto.Name);
        Assert.Equal("Test Description", dishResponseDto.Description);
        Assert.Equal("testurl.com", dishResponseDto.PhotoUrl);
    }

    [Fact]
    public void DishResponseDto_Creation_Failure()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var dishResponseDto = new DishResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, Name = "Test Dish", Description = "Test Description", PhotoUrl = "testurl.com" };

        Assert.NotEqual("WrongDish", dishResponseDto.Name);
    }

}
