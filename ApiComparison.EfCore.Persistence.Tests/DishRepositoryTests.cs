using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace ApiComparison.EfCore.Persistence.Tests;

public class DishRepositoryTests
{
    private readonly Mock<ApiComparisonDbContext> _dbContextMock;
    private readonly DishRepository _repository;

    public DishRepositoryTests()
    {
        _dbContextMock = new Mock<ApiComparisonDbContext>();
        _repository = new DishRepository(_dbContextMock.Object);
    }

    // GetByIdAsync
    [Fact]
    public async Task DishRepository_GetByIdAsync_ReturnsDish()
    {
        // Arrange
        var dishId = Guid.NewGuid();
        var dish = new Dish
        {
            Id = dishId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "Test Dish",
            Description = "Test Description",
            PhotoUrl = "https://example.com/photo.jpg",
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Id = Guid.NewGuid(), Name = "Ingredient 1", Quantity = 1.5, UnitOfMeasure = "g" },
                new Ingredient { Id = Guid.NewGuid(), Name = "Ingredient 2", Quantity = 2.0, UnitOfMeasure = "kg" }
            }
        };
        var dishes = new List<Dish> { dish };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Dishes)
            .ReturnsDbSet(dishes);

        // Act
        var result = await _repository.GetByIdAsync(dishId, cancellationToken);

        // Assert
        Assert.Equal(dish, result);
    }

    [Fact]
    public async Task DishRepository_GetByIdAsync_ReturnsNull()
    {
        // Arrange
        var dishId = Guid.NewGuid();
        var dishes = new List<Dish>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Dishes)
            .ReturnsDbSet(dishes);

        // Act
        var result = await _repository.GetByIdAsync(dishId, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    // GetAllAsync
    [Fact]
    public async Task DishRepository_GetAllAsync_ReturnsDishes()
    {
        // Arrange
        var dishes = new List<Dish>
        {
            new Dish
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Name = "Dish 1",
                Description = "Description 1",
                PhotoUrl = "https://example.com/photo1.jpg",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Id = Guid.NewGuid(), Name = "Ingredient 1", Quantity = 1.5, UnitOfMeasure = "g" },
                    new Ingredient { Id = Guid.NewGuid(), Name = "Ingredient 2", Quantity = 2.0, UnitOfMeasure = "kg" }
                }
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Name = "Dish 2",
                Description = "Description 2",
                PhotoUrl = "https://example.com/photo2.jpg",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Id = Guid.NewGuid(), Name = "Ingredient 3", Quantity = 2.5, UnitOfMeasure = "g" },
                    new Ingredient { Id = Guid.NewGuid(), Name = "Ingredient 4", Quantity = 3.0, UnitOfMeasure = "kg" }
                }
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Dishes)
            .ReturnsDbSet(dishes);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Equal(dishes, result);
    }

    [Fact]
    public async Task DishRepository_GetAllAsync_ReturnsEmptyList()
    {
        // Arrange
        var dishes = new List<Dish>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Dishes)
            .ReturnsDbSet(dishes);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task DishRepository_UpdateAsync_DoesNotUpdateDish()
    {
        // Arrange
        var existingDish = new Dish
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "Old Dish",
            Description = "Old Description",
            PhotoUrl = "https://example.com/old-photo.jpg",
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Id = Guid.NewGuid(), Name = "Old Ingredient 1", Quantity = 1.0, UnitOfMeasure = "g" },
                new Ingredient { Id = Guid.NewGuid(), Name = "Old Ingredient 2", Quantity = 2.0, UnitOfMeasure = "kg" }
            }
        };
        var updatedDish = new Dish
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "New Dish",
            Description = "New Description",
            PhotoUrl = "https://example.com/new-photo.jpg",
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Id = Guid.NewGuid(), Name = "New Ingredient 1", Quantity = 2.0, UnitOfMeasure = "g" },
                new Ingredient { Id = Guid.NewGuid(), Name = "New Ingredient 2", Quantity = 3.0, UnitOfMeasure = "kg" }
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Dishes.FindAsync(updatedDish.Id, cancellationToken))
            .ReturnsAsync((Dish)null);

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(1);

        // Act
        await _repository.UpdateAsync(updatedDish, cancellationToken);

        // Assert
        Assert.NotEqual(updatedDish.Name, existingDish.Name);
        Assert.NotEqual(updatedDish.Description, existingDish.Description);
        Assert.NotEqual(updatedDish.PhotoUrl, existingDish.PhotoUrl);
    }

    // DisposeAsync
    [Fact]
    public async Task DishRepository_DisposeAsync_CallsDbContextDisposeAsync()
    {
        // Act
        await _repository.DisposeAsync();

        // Assert
        _dbContextMock.Verify(m => m.DisposeAsync(), Times.Once);
    }
}
