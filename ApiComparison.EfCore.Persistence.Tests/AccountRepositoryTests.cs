using ApiComparison.Domain.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using ApiComparison.EfCore.Persistence.Repositories;
using ApiComparison.Domain.Entities;
using Moq.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Tests;

public class AccountRepositoryTests
{
    private readonly Mock<ApiComparisonDbContext> _dbContextMock;
    private readonly AccountRepository _repository;

    public AccountRepositoryTests()
    {
        _dbContextMock = new Mock<ApiComparisonDbContext>();
        _repository = new AccountRepository(_dbContextMock.Object);
    }

    // GetByIdAsync
    [Fact]
    public async Task AccountRepository_GetByIdAsync_ReturnsAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new Account
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        };
        var accounts = new List<Account> { account };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Accounts)
            .ReturnsDbSet(accounts);
        var dbAccounts =  _dbContextMock.Object.Accounts;
        // Act
        var result = await _repository.GetByIdAsync(dbAccounts.SingleOrDefault()!.Id, cancellationToken);

        // Assert
        Assert.Equal(account, result);
    }

    [Fact]
    public async Task AccountRepository_GetByIdAsync_ReturnsNull()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var accounts = new List<Account>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.SetupGet(m => m.Accounts)
            .ReturnsDbSet(accounts);

        // Act
        var result = await _repository.GetByIdAsync(accountId, cancellationToken);

        // Assert
        Assert.Null(result);
    }

    // GetAllAsync
    [Fact]
    public async Task AccountRepository_GetAllAsync_ReturnsAccounts()
    {
        // Arrange
        var accounts = new List<Account>
        {
            new Account
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        },
         new Account
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Username = "testuser",
            Password = "testpassword",
            Email = "test@example.com"
        } 
        };

        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Accounts).ReturnsDbSet(accounts);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Equal(accounts, result);
    }

    [Fact]
    public async Task AccountRepository_GetAllAsync_ReturnsEmptyList()
    {
        // Arrange
        var accounts = new List<Account>();
        var cancellationToken = new CancellationToken();

        _dbContextMock.SetupGet(m => m.Accounts).ReturnsDbSet(accounts);

        // Act
        var result = await _repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task AccountRepository_UpdateAsync_DoesNotUpdateAccount()
    {
        // Arrange
        var existingAccount = new Account
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Username = "OldUsername",
            Password = "testpassword",
            Email = "test@example.com"
        };
        var updatedAccount = new Account
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            Username = "NewUsername",
            Password = "testpassword",
            Email = "test@example.com"
        };
        var cancellationToken = new CancellationToken();

        _dbContextMock.Setup(m => m.Accounts.FindAsync(updatedAccount.Id, cancellationToken))
            .ReturnsAsync((Account)null);

        _dbContextMock.Setup(m => m.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(1);

        // Act
        await _repository.UpdateAsync(updatedAccount, cancellationToken);

        // Assert
        Assert.NotEqual(updatedAccount.Username, existingAccount.Username);
    }

    // DisposeAsync
    [Fact]
    public async Task AccountRepository_DisposeAsync_CallsDbContextDisposeAsync()
    {
        // Act
        await _repository.DisposeAsync();

        // Assert
        _dbContextMock.Verify(m => m.DisposeAsync(), Times.Once);
    }
}
