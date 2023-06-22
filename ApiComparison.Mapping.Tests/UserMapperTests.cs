using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Xunit;

namespace ApiComparison.Mapping.Tests;

public class UserMapperTests
{
    [Fact]
    public void UserMapper_EntityToResponse_Pass()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Bio = "Test Bio"
        };
        var mapper = new UserMapper();

        // Act
        var result = mapper.EntityToResponse(user);

        // Assert
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.Bio, result.Bio);
    }

    [Fact]
    public void UserMapper_EntityToResponse_Fail()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Bio = "Test Bio"
        };
        var mapper = new UserMapper();

        // Act
        var result = mapper.EntityToResponse(user);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    // UserMapper - RequestToEntity (UserPutRequestDto)
    [Fact]
    public void UserMapper_RequestToEntity_Pass()
    {
        // Arrange
        var requestDto = new UserPutRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Bio = "Test Bio"
        };
        var mapper = new UserMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.Equal(requestDto.FirstName, result.FirstName);
        Assert.Equal(requestDto.LastName, result.LastName);
        Assert.Equal(requestDto.Bio, result.Bio);
    }

    [Fact]
    public void UserMapper_RequestToEntity_Fail()
    {
        // Arrange
        var requestDto = new UserPutRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Bio = "Test Bio"
        };
        var mapper = new UserMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.NotEqual("WrongUser", result.FirstName);
    }

    // UserMapper - RequestToEntity (UserCreateRequestDto)
    [Fact]
    public void UserMapper_RequestToEntity_UserCreateRequest_Pass()
    {
        // Arrange
        var requestDto = new UserCreateRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Bio = "Test Bio",
            Email = "test@example.com",
            Username = "testuser",
            Password = "testpassword",
            Street = "Test Street",
            StreetNumber = "100",
            City = "TestCity"
        };
        var mapper = new UserMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.Equal(requestDto.FirstName, result.FirstName);
        Assert.Equal(requestDto.LastName, result.LastName);
        Assert.Equal(requestDto.Bio, result.Bio);
        Assert.Equal(requestDto.Email, result.Account.Email);
        Assert.Equal(requestDto.Username, result.Account.Username);
        Assert.Equal(requestDto.Password, result.Account.Password);
        Assert.Equal(requestDto.Street, result.Address.Street);
        Assert.Equal(requestDto.StreetNumber, result.Address.StreetNumber);
        Assert.Equal(requestDto.City, result.Address.City);
    }

    [Fact]
    public void UserMapper_RequestToEntity_UserCreateRequest_Fail()
    {
        // Arrange
        var requestDto = new UserCreateRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Bio = "Test Bio",
            Email = "test@example.com",
            Username = "testuser",
            Password = "testpassword",
            Street = "Test Street",
            StreetNumber = "100",
            City = "TestCity"
        };
        var mapper = new UserMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.NotEqual("WrongUser", result.FirstName);
    }
}

