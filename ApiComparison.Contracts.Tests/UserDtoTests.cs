using ApiComparison.Contracts.UserDtos;
using Xunit;

namespace ApiComparison.Contracts.Tests;

public class UserDtoTests
{
    [Fact]
    public void UserCreateRequestDto_Creation_Success()
    {
        var userCreateRequestDto = new UserCreateRequestDto() { FirstName = "Test", LastName = "User", Bio = "Test Bio", Username = "TestUser", Password = "TestPassword", Email = "test@mail.com", Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        Assert.Equal("Test", userCreateRequestDto.FirstName);
        Assert.Equal("User", userCreateRequestDto.LastName);
        Assert.Equal("Test Bio", userCreateRequestDto.Bio);
        Assert.Equal("TestUser", userCreateRequestDto.Username);
        Assert.Equal("TestPassword", userCreateRequestDto.Password);
        Assert.Equal("test@mail.com", userCreateRequestDto.Email);
        Assert.Equal("Test Street", userCreateRequestDto.Street);
        Assert.Equal("100", userCreateRequestDto.StreetNumber);
        Assert.Equal("TestCity", userCreateRequestDto.City);
    }

    [Fact]
    public void UserCreateRequestDto_Creation_Failure()
    {
        var userCreateRequestDto = new UserCreateRequestDto() { FirstName = "Test", LastName = "User", Bio = "Test Bio", Username = "TestUser", Password = "TestPassword", Email = "test@mail.com", Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        Assert.NotEqual("WrongUser", userCreateRequestDto.FirstName);
    }

    [Fact]
    public void UserResponseDto_Creation_Success()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var userResponseDto = new UserResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, FirstName = "Test", LastName = "User", Bio = "Test Bio" };

        Assert.Equal(id, userResponseDto.Id);
        Assert.Equal(now, userResponseDto.CreatedAt);
        Assert.Equal(now, userResponseDto.LastUpdatedAt);
        Assert.Equal("Test", userResponseDto.FirstName);
        Assert.Equal("User", userResponseDto.LastName);
        Assert.Equal("Test Bio", userResponseDto.Bio);
    }

    [Fact]
    public void UserResponseDto_Creation_Failure()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var userResponseDto = new UserResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, FirstName = "Test", LastName = "User", Bio = "Test Bio" };

        Assert.NotEqual("WrongUser", userResponseDto.FirstName);
    }
}
