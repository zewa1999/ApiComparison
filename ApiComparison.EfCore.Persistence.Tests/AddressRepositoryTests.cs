using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace ApiComparison.EfCore.Persistence.Tests;

public class AddressRepositoryTests
{
    private readonly Mock<ApiComparisonDbContext> _dbContextMock;
    private readonly AddressRepository _repository;

    public AddressRepositoryTests()
    {
        _dbContextMock = new Mock<ApiComparisonDbContext>();
        _repository = new AddressRepository(_dbContextMock.Object);
    }

    // GetByIdAsync
    [Fact]
    public async Task AddressRepository_GetByIdAsync_ReturnsAddress()
    {
        // Arrange
        var addressId = Guid.NewGuid();
        var address = new Address
        {
            Id = addressId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Street = "Test Street",
            StreetNumber = "123",
            City = "Test City"
        };
        var addresses = new List<Address> { address };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Addresses)
            .ReturnsDbSet(addresses);

        // Act
        var result = await _repository.GetByIdAsync(addressId, cancellationToken);

        // Assert
        Assert.Equal(address, result);
    }

    [Fact]
    public async Task AddressRepository_GetByIdAsync_ReturnsNull()
    {
        // Arrange
        var addressId = Guid.NewGuid();
        var addresses = new List<Address>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Addresses)
            .ReturnsDbSet(addresses);

        // Act
        var result = await _repository.GetByIdAsync(addressId, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    // GetAllAsync
    [Fact]
    public async Task AddressRepository_GetAllAsync_ReturnsAddresses()
    {
        // Arrange
        var addresses = new List<Address>
        {
            new Address
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Street = "Test Street 1",
                StreetNumber = "123",
                City = "Test City 1"
            },
            new Address
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Street = "Test Street 2",
                StreetNumber = "456",
                City = "Test City 2"
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Addresses)
            .ReturnsDbSet(addresses);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Equal(addresses, result);
    }

    [Fact]
    public async Task AddressRepository_GetAllAsync_ReturnsEmptyList()
    {
        // Arrange
        var addresses = new List<Address>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Addresses)
            .ReturnsDbSet(addresses);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task AddressRepository_UpdateAsync_DoesNotUpdateAddress()
    {
        // Arrange
        var existingAddress = new Address
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Street = "Old Street",
            StreetNumber = "123",
            City = "Old City"
        };
        var updatedAddress = new Address
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Street = "New Street",
            StreetNumber = "456",
            City = "New City"
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Addresses.FindAsync(updatedAddress.Id, cancellationToken))
            .ReturnsAsync((Address)null!);

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(1);

        // Act
        await _repository.UpdateAsync(updatedAddress, cancellationToken);

        // Assert
        Assert.NotEqual(updatedAddress.Street, existingAddress.Street);
        Assert.NotEqual(updatedAddress.City, existingAddress.City);
    }

    // DisposeAsync
    [Fact]
    public async Task AddressRepository_DisposeAsync_CallsDbContextDisposeAsync()
    {
        // Act
        await _repository.DisposeAsync();

        // Assert
        _dbContextMock.Verify(m => m.DisposeAsync(), Times.Once);
    }
}
