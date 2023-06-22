using ApiComparison.Contracts.AccountDtos;
using Xunit;

namespace ApiComparison.Contracts.Tests;

public class AccountDtoTests
{
    [Fact]
    public void AccountRequestDto_Creation_Success()
    {
        var accountRequestDto = new AccountRequestDto() { Username = "TestUser", Password = "TestPassword", Email = "test@mail.com" };

        Assert.Equal("TestUser", accountRequestDto.Username);
        Assert.Equal("TestPassword", accountRequestDto.Password);
        Assert.Equal("test@mail.com", accountRequestDto.Email);
    }

    [Fact]
    public void AccountRequestDto_Creation_Failure()
    {
        var accountRequestDto = new AccountRequestDto() { Username = "TestUser", Password = "TestPassword", Email = "test@mail.com" };

        Assert.NotEqual("wrongTestUser", accountRequestDto.Username);
    }

    [Fact]
    public void AccountResponseDto_Creation_Success()
    {
        var accountResponseDto = new AccountResponseDto() { Id = Guid.NewGuid(), CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, Username = "TestUser", Email = "test@mail.com" };

        Assert.NotEqual(Guid.Empty, accountResponseDto.Id);
        Assert.NotEqual(default(DateTime), accountResponseDto.CreatedAt);
        Assert.NotEqual(default(DateTime), accountResponseDto.LastUpdatedAt);
        Assert.Equal("TestUser", accountResponseDto.Username);
        Assert.Equal("test@mail.com", accountResponseDto.Email);
    }

    [Fact]
    public void AccountResponseDto_Creation_Failure()
    {
        var accountResponseDto = new AccountResponseDto() { Id = Guid.NewGuid(), CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, Username = "TestUser", Email = "test@mail.com" };

        Assert.NotEqual("wrongTestUser", accountResponseDto.Username);
    }
}
