using ApiComparison.Domain.Entities;
using Xunit;

namespace ApiComparison.Domain.Tests;

public class UserTests
{

    [Fact]
    public void User_Properties_Pass()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            FirstName = "John",
            LastName = "Doe",
            Bio = "Test Bio",
            AccountId = Guid.NewGuid(),
            Account = new Account
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Username = "testuser",
                Password = "testpassword",
                Email = "test@example.com"
            },
            AddressId = Guid.NewGuid(),
            Address = new Address
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Street = "Test Street",
                StreetNumber = "100",
                City = "Test City"
            },
            Dishes = new List<Dish>()
        };

        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.NotEqual(default(DateTime), user.CreatedAt);
        Assert.NotEqual(default(DateTime), user.LastUpdatedAt);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
        Assert.Equal("Test Bio", user.Bio);
        Assert.NotEqual(Guid.Empty, user.AccountId);
        Assert.NotNull(user.Account);
        Assert.NotEqual(Guid.Empty, user.AddressId);
        Assert.NotNull(user.Address);
        Assert.NotNull(user.Dishes);
    }

    [Fact]
    public void User_Properties_Fail()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.Empty,
            CreatedAt = default(DateTime),
            LastUpdatedAt = default(DateTime),
            FirstName = "John",
            LastName = "Doe",
            Bio = "Test Bio",
            AccountId = Guid.Empty,
            Account = null!,
            AddressId = Guid.Empty,
            Address = null!,
            Dishes = null!
        };

        // Assert
        Assert.Equal(Guid.Empty, user.Id);
        Assert.Equal(default(DateTime), user.CreatedAt);
        Assert.Equal(default(DateTime), user.LastUpdatedAt);
        Assert.NotEqual("Wrong First Name", user.FirstName);
        Assert.NotEqual("Wrong Last Name", user.LastName);
        Assert.NotEqual("Wrong Bio", user.Bio);
        Assert.Equal(Guid.Empty, user.AccountId);
        Assert.Null(user.Account);
        Assert.Equal(Guid.Empty, user.AddressId);
        Assert.Null(user.Address);
        Assert.Null(user.Dishes);
    }
}
