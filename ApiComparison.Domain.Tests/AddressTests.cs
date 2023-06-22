using ApiComparison.Domain.Entities;
using Xunit;

namespace ApiComparison.Domain.Tests;

public class AddressTests
{
    [Fact]
    public void Address_Properties_Pass()
    {
        // Arrange
        var address = new Address
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Street = "Test Street",
            StreetNumber = "100",
            City = "Test City"
        };

        // Assert
        Assert.NotEqual(Guid.Empty, address.Id);
        Assert.NotEqual(default(DateTime), address.CreatedAt);
        Assert.NotEqual(default(DateTime), address.LastUpdatedAt);
        Assert.Equal("Test Street", address.Street);
        Assert.Equal("100", address.StreetNumber);
        Assert.Equal("Test City", address.City);
    }

    [Fact]
    public void Address_Properties_Fail()
    {
        // Arrange
        var address = new Address
        {
            Id = Guid.Empty,
            CreatedAt = default(DateTime),
            LastUpdatedAt = default(DateTime),
            Street = "Test Street",
            StreetNumber = "100",
            City = "Test City"
        };

        // Assert
        Assert.Equal(Guid.Empty, address.Id);
        Assert.Equal(default(DateTime), address.CreatedAt);
        Assert.Equal(default(DateTime), address.LastUpdatedAt);
        Assert.NotEqual("Wrong Street", address.Street);
        Assert.NotEqual("200", address.StreetNumber);
        Assert.NotEqual("Wrong City", address.City);
    }
}
