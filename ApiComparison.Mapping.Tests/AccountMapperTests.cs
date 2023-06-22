using ApiComparison.Contracts.AccountDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Xunit;

namespace ApiComparison.Mapping.Tests;

public class AccountMapperTests
{
    [Fact]
    public void AccountMapper_RequestToEntity_Pass()
    {
        // Arrange
        var requestDto = new AccountRequestDto
        {
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        };
        var mapper = new AccountMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.Equal(requestDto.Username, result.Username);
        Assert.Equal(requestDto.Password, result.Password);
        Assert.Equal(requestDto.Email, result.Email);
    }

    [Fact]
    public void AccountMapper_RequestToEntity_Fail()
    {
        // Arrange
        var requestDto = new AccountRequestDto
        {
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        };
        var mapper = new AccountMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.NotEqual("wrongusername", result.Username);
    }

    [Fact]
    public void AccountMapper_EntityToResponse_Pass()
    {
        // Arrange
        var account = new Account
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            Password= "password",
        };
        var mapper = new AccountMapper();

        // Act
        var result = mapper.EntityToResponse(account);

        // Assert
        Assert.Equal(account.Id, result.Id);
        Assert.Equal(account.Username, result.Username);
        Assert.Equal(account.Email, result.Email);
    }

    [Fact]
    public void AccountMapper_EntityToResponse_Fail()
    {
        // Arrange
        var account = new Account
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Email = "test@example.com",
            Password = "password"
        };
        var mapper = new AccountMapper();

        // Act
        var result = mapper.EntityToResponse(account);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
    }
}
