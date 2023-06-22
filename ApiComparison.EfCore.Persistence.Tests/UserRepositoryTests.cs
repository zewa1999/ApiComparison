using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace ApiComparison.EfCore.Persistence.Tests;

public class UserRepositoryTests
{
    private readonly Mock<ApiComparisonDbContext> _dbContextMock;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        _dbContextMock = new Mock<ApiComparisonDbContext>();
        _repository = new UserRepository(_dbContextMock.Object);
    }

    // GetByIdAsync
    [Fact]
    public async Task UserRepository_GetByIdAsync_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            FirstName = "John",
            LastName = "Doe",
            Bio = "Test bio",
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
                StreetNumber = "123",
                City = "Test City"
            },
            Dishes = new List<Dish>
            {
                new Dish { Id = Guid.NewGuid(), Name = "Dish 1", Description = "Description 1", PhotoUrl = "https://example.com/dish1.jpg" },
                new Dish { Id = Guid.NewGuid(), Name = "Dish 2", Description = "Description 2", PhotoUrl = "https://example.com/dish2.jpg" }
            }
        };
        var users = new List<User> { user };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Users)
            .ReturnsDbSet(users);

        // Act
        var result = await _repository.GetByIdAsync(userId, cancellationToken);

        // Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task UserRepository_GetByIdAsync_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = new List<User>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Users)
            .ReturnsDbSet(users);

        // Act
        var result = await _repository.GetByIdAsync(userId, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    // GetAllAsync
    [Fact]
    public async Task UserRepository_GetAllAsync_ReturnsUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                FirstName = "John",
                LastName = "Doe",
                Bio = "Test bio",
                AccountId = Guid.NewGuid(),
                Account = new Account
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    Username = "testuser1",
                    Password = "testpassword1",
                    Email = "test1@example.com"
                },
                AddressId = Guid.NewGuid(),
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    Street = "Test Street 1",
                    StreetNumber = "123",
                    City = "Test City 1"
                },
                Dishes = new List<Dish>
                {
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 1", Description = "Description 1", PhotoUrl = "https://example.com/dish1.jpg" },
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 2", Description = "Description 2", PhotoUrl = "https://example.com/dish2.jpg" }
                }
            },
            new User
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                FirstName = "Jane",
                LastName = "Smith",
                Bio = "Test bio 2",
                AccountId = Guid.NewGuid(),
                Account = new Account
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    Username = "testuser2",
                    Password = "testpassword2",
                    Email = "test2@example.com"
                },
                AddressId = Guid.NewGuid(),
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                    Street = "Test Street 2",
                    StreetNumber = "456",
                    City = "Test City 2"
                },
                Dishes = new List<Dish>
                {
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 3", Description = "Description 3", PhotoUrl = "https://example.com/dish3.jpg" },
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 4", Description = "Description 4", PhotoUrl = "https://example.com/dish4.jpg" }
                }
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Users)
            .ReturnsDbSet(users);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Equal(users, result);
    }

    [Fact]
    public async Task UserRepository_GetAllAsync_ReturnsEmptyList()
    {
        // Arrange
        var users = new List<User>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Users)
            .ReturnsDbSet(users);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task UserRepository_UpdateAsync_DoesNotUpdateUser()
    {
        // Arrange
        var existingUser = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            FirstName = "OldFirstName",
            LastName = "OldLastName",
            Bio = "Old bio",
            AccountId = Guid.NewGuid(),
            Account = new Account
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Username = "olduser",
                Password = "oldpassword",
                Email = "old@example.com"
            },
            AddressId = Guid.NewGuid(),
            Address = new Address
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Street = "Old Street",
                StreetNumber = "123",
                City = "Old City"
            },
            Dishes = new List<Dish>
            {
                new Dish { Id = Guid.NewGuid(), Name = "Old Dish 1", Description = "Old Description 1", PhotoUrl = "https://example.com/old-dish1.jpg" },
                new Dish { Id = Guid.NewGuid(), Name = "Old Dish 2", Description = "Old Description 2", PhotoUrl = "https://example.com/old-dish2.jpg" }
            }
        };
        var updatedUser = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            Bio = "New bio",
            AccountId = Guid.NewGuid(),
            Account = new Account
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Username = "newuser",
                Password = "newpassword",
                Email = "new@example.com"
            },
            AddressId = Guid.NewGuid(),
            Address = new Address
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Street = "New Street",
                StreetNumber = "456",
                City = "New City"
            },
            Dishes = new List<Dish>
            {
                new Dish { Id = Guid.NewGuid(), Name = "New Dish 1", Description = "New Description 1", PhotoUrl = "https://example.com/new-dish1.jpg" },
                new Dish { Id = Guid.NewGuid(), Name = "New Dish 2", Description = "New Description 2", PhotoUrl = "https://example.com/new-dish2.jpg" }
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Users.FindAsync(updatedUser.Id, cancellationToken))
            .ReturnsAsync((User)null!);

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(1);

        // Act
        await _repository.UpdateAsync(updatedUser, cancellationToken);

        // Assert
        Assert.NotEqual(updatedUser.FirstName, existingUser.FirstName);
        Assert.NotEqual(updatedUser.LastName, existingUser.LastName);
        Assert.NotEqual(updatedUser.Bio, existingUser.Bio);
        Assert.NotEqual(updatedUser.Account.Username, existingUser.Account.Username);
        Assert.NotEqual(updatedUser.Account.Password, existingUser.Account.Password);
        Assert.NotEqual(updatedUser.Account.Email, existingUser.Account.Email);
        Assert.NotEqual(updatedUser.Address.Street, existingUser.Address.Street);
        Assert.NotEqual(updatedUser.Address.StreetNumber, existingUser.Address.StreetNumber);
        Assert.NotEqual(updatedUser.Address.City, existingUser.Address.City);
        Assert.NotEqual(updatedUser.Dishes, existingUser.Dishes);
    }

    // DisposeAsync
    [Fact]
    public async Task UserRepository_DisposeAsync_CallsDbContextDisposeAsync()
    {
        // Act
        await _repository.DisposeAsync();

        // Assert
        _dbContextMock.Verify(m => m.DisposeAsync(), Times.Once);
    }
}
