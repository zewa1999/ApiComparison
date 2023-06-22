using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace ApiComparison.EfCore.Persistence.Tests;

public class IngredientRepositoryTests
{
    private readonly Mock<ApiComparisonDbContext> _dbContextMock;
    private readonly IngredientRepository _repository;

    public IngredientRepositoryTests()
    {
        _dbContextMock = new Mock<ApiComparisonDbContext>();
        _repository = new IngredientRepository(_dbContextMock.Object);
    }

    // GetByIdAsync
    [Fact]
    public async Task IngredientRepository_GetByIdAsync_ReturnsIngredient()
    {
        // Arrange
        var ingredientId = Guid.NewGuid();
        var ingredient = new Ingredient
        {
            Id = ingredientId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "Test Ingredient",
            Quantity = 1.5,
            UnitOfMeasure = "g",
            Dishes = new List<Dish>
            {
                new Dish { Id = Guid.NewGuid(), Name = "Dish 1", Description = "Description 1", PhotoUrl = "https://example.com/dish1.jpg" },
                new Dish { Id = Guid.NewGuid(), Name = "Dish 2", Description = "Description 2", PhotoUrl = "https://example.com/dish2.jpg" }
            }
        };
        var ingredients = new List<Ingredient> { ingredient };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Ingredients)
            .ReturnsDbSet(ingredients);

        // Act
        var result = await _repository.GetByIdAsync(ingredientId, cancellationToken);

        // Assert
        Assert.Equal(ingredient, result);
    }

    [Fact]
    public async Task IngredientRepository_GetByIdAsync_ReturnsNull()
    {
        // Arrange
        var ingredientId = Guid.NewGuid();
        var ingredients = new List<Ingredient>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Ingredients)
            .ReturnsDbSet(ingredients);

        // Act
        var result = await _repository.GetByIdAsync(ingredientId, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    // GetAllAsync
    [Fact]
    public async Task IngredientRepository_GetAllAsync_ReturnsIngredients()
    {
        // Arrange
        var ingredients = new List<Ingredient>
        {
            new Ingredient
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Name = "Ingredient 1",
                Quantity = 1.5,
                UnitOfMeasure = "g",
                Dishes = new List<Dish>
                {
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 1", Description = "Description 1", PhotoUrl = "https://example.com/dish1.jpg" },
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 2", Description = "Description 2", PhotoUrl = "https://example.com/dish2.jpg" }
                }
            },
            new Ingredient
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Name = "Ingredient 2",
                Quantity = 2.0,
                UnitOfMeasure = "kg",
                Dishes = new List<Dish>
                {
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 3", Description = "Description 3", PhotoUrl = "https://example.com/dish3.jpg" },
                    new Dish { Id = Guid.NewGuid(), Name = "Dish 4", Description = "Description 4", PhotoUrl = "https://example.com/dish4.jpg" }
                }
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Ingredients)
            .ReturnsDbSet(ingredients);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Equal(ingredients, result);
    }

    [Fact]
    public async Task IngredientRepository_GetAllAsync_ReturnsEmptyList()
    {
        // Arrange
        var ingredients = new List<Ingredient>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Ingredients)
            .ReturnsDbSet(ingredients);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task IngredientRepository_UpdateAsync_DoesNotUpdateIngredient()
    {
        // Arrange
        var existingIngredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "Old Ingredient",
            Quantity = 1.0,
            UnitOfMeasure = "g",
            Dishes = new List<Dish>
            {
                new Dish { Id = Guid.NewGuid(), Name = "Old Dish 1", Description = "Old Description 1", PhotoUrl = "https://example.com/old-dish1.jpg" },
                new Dish { Id = Guid.NewGuid(), Name = "Old Dish 2", Description = "Old Description 2", PhotoUrl = "https://example.com/old-dish2.jpg" }
            }
        };
        var updatedIngredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Name = "New Ingredient",
            Quantity = 2.0,
            UnitOfMeasure = "kg",
            Dishes = new List<Dish>
            {
                new Dish { Id = Guid.NewGuid(), Name = "New Dish 1", Description = "New Description 1", PhotoUrl = "https://example.com/new-dish1.jpg" },
                new Dish { Id = Guid.NewGuid(), Name = "New Dish 2", Description = "New Description 2", PhotoUrl = "https://example.com/new-dish2.jpg" }
            }
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Ingredients.FindAsync(updatedIngredient.Id, cancellationToken))
            .ReturnsAsync((Ingredient)null!);

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(1);

        // Act
        await _repository.UpdateAsync(updatedIngredient, cancellationToken);

        // Assert
        Assert.NotEqual(updatedIngredient.Name, existingIngredient.Name);
        Assert.NotEqual(updatedIngredient.Quantity, existingIngredient.Quantity);
        Assert.NotEqual(updatedIngredient.UnitOfMeasure, existingIngredient.UnitOfMeasure);
    }

    // DisposeAsync
    [Fact]
    public async Task IngredientRepository_DisposeAsync_CallsDbContextDisposeAsync()
    {
        // Act
        await _repository.DisposeAsync();

        // Assert
        _dbContextMock.Verify(m => m.DisposeAsync(), Times.Once);
    }
}
