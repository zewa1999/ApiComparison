using ApiComparison.Domain.Entities;
using Xunit;

namespace ApiComparison.Domain.Tests;

public class AccountTests
{

    [Fact]
    public void Account_Properties_Pass()
    {
        // Arrange
        var account = new Account
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        };

        // Assert
        Assert.NotEqual(Guid.Empty, account.Id);
        Assert.NotEqual(default(DateTime), account.CreatedAt);
        Assert.NotEqual(default(DateTime), account.LastUpdatedAt);
        Assert.Equal("testuser", account.Username);
        Assert.Equal("testpassword", account.Password);
        Assert.Equal("test@example.com", account.Email);
    }

    [Fact]
    public void Account_Properties_Fail()
    {
        // Arrange
        var account = new Account
        {
            Id = Guid.Empty,
            CreatedAt = default(DateTime),
            LastUpdatedAt = default(DateTime),
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        };

        // Assert
        Assert.Equal(Guid.Empty, account.Id);
        Assert.Equal(default(DateTime), account.CreatedAt);
        Assert.Equal(default(DateTime), account.LastUpdatedAt);
        Assert.NotEqual("wronguser", account.Username);
        Assert.NotEqual("wrongpassword", account.Password);
        Assert.NotEqual("wrong@example.com", account.Email);
    }
}
