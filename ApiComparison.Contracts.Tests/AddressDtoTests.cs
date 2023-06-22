using ApiComparison.Contracts.AddressDtos;
using Xunit;

namespace ApiComparison.Contracts.Tests;

public class AddressDtoTests
{
    [Fact]
    public void AddressRequestDto_Creation_Success()
    {
        var addressRequestDto = new AddressRequestDto() { Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        Assert.Equal("Test Street", addressRequestDto.Street);
        Assert.Equal("100", addressRequestDto.StreetNumber);
        Assert.Equal("TestCity", addressRequestDto.City);
    }

    [Fact]
    public void AddressRequestDto_Creation_Failure()
    {
        var addressRequestDto = new AddressRequestDto() { Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        Assert.NotEqual("WrongStreet", addressRequestDto.Street);
    }

    [Fact]
    public void AddressResponseDto_Creation_Success()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var addressResponseDto = new AddressResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        Assert.Equal(id, addressResponseDto.Id);
        Assert.Equal(now, addressResponseDto.CreatedAt);
        Assert.Equal(now, addressResponseDto.LastUpdatedAt);
        Assert.Equal("Test Street", addressResponseDto.Street);
        Assert.Equal("100", addressResponseDto.StreetNumber);
        Assert.Equal("TestCity", addressResponseDto.City);
    }

    [Fact]
    public void AddressResponseDto_Creation_Failure()
    {
        var id = Guid.NewGuid();
        var now = DateTime.Now;
        var addressResponseDto = new AddressResponseDto() { Id = id, CreatedAt = now, LastUpdatedAt = now, Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        Assert.NotEqual("WrongStreet", addressResponseDto.Street);
    }
}
