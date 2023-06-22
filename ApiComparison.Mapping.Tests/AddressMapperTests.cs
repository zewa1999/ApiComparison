using ApiComparison.Contracts.AddressDtos;
using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Mappers;
using Xunit;

namespace ApiComparison.Mapping.Tests;

public class AddressMapperTests
{
    [Fact]
    public void AddressMapper_RequestToEntity_Pass()
    {
        // Arrange
        var requestDto = new AddressRequestDto
        {
            Street = "Test Street",
            StreetNumber = "100",
            City = "TestCity"
        };
        var mapper = new AddressMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.Equal(requestDto.Street, result.Street);
        Assert.Equal(requestDto.StreetNumber, result.StreetNumber);
        Assert.Equal(requestDto.City, result.City);
    }

    [Fact]
    public void AddressMapper_RequestToEntity_Fail()
    {
        // Arrange
        var requestDto = new AddressRequestDto
        {
            Street = "Test Street",
            StreetNumber = "100",
            City = "TestCity"
        };
        var mapper = new AddressMapper();

        // Act
        var result = mapper.RequestToEntity(requestDto);

        // Assert
        Assert.NotEqual("WrongStreet", result.Street);
    }

    // AddressMapper - EntityToResponse
    [Fact]
    public void AddressMapper_EntityToResponse_Pass()
    {
        // Arrange
        var address = new Address
        {
            Id = Guid.NewGuid(),
            Street = "Test Street",
            StreetNumber = "100",
            City = "TestCity"
        };
        var mapper = new AddressMapper();

        // Act
        var result = mapper.EntityToResponse(address);

        // Assert
        Assert.Equal(address.Id, result.Id);
        Assert.Equal(address.Street, result.Street);
        Assert.Equal(address.StreetNumber, result.StreetNumber);
        Assert.Equal(address.City, result.City);
    }

    [Fact]
    public void AddressMapper_EntityToResponse_Fail()
    {
        // Arrange
        var address = new Address
        {
            Id = Guid.NewGuid(),
            Street = "Test Street",
            StreetNumber = "100",
            City = "TestCity"
        };
        var mapper = new AddressMapper();

        // Act
        var result = mapper.EntityToResponse(address);

        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    // AddressMapper - UserRequestToAddressRequest
    [Fact]
    public void AddressMapper_UserRequestToAddressRequest_Pass()
    {
        // Arrange
        var userRequestDto = new UserCreateRequestDto() { FirstName = "Test", LastName = "User", Bio = "Test Bio", Username = "TestUser", Password = "TestPassword", Email = "test@mail.com", Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        var mapper = new AddressMapper();

        // Act
        var result = mapper.UserRequestToAddressRequest(userRequestDto);

        // Assert
        Assert.Equal(userRequestDto.Street, result.Street);
        Assert.Equal(userRequestDto.StreetNumber, result.StreetNumber);
        Assert.Equal(userRequestDto.City, result.City);
    }

    [Fact]
    public void AddressMapper_UserRequestToAddressRequest_Fail()
    {
        // Arrange
        var userRequestDto = new UserCreateRequestDto() { FirstName = "Test", LastName = "User", Bio = "Test Bio", Username = "TestUser", Password = "TestPassword", Email = "test@mail.com", Street = "Test Street", StreetNumber = "100", City = "TestCity" };

        var mapper = new AddressMapper();

        // Act
        var result = mapper.UserRequestToAddressRequest(userRequestDto);

        // Assert
        Assert.NotEqual("WrongStreet", result.Street);
    }
}
