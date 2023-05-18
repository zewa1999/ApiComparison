using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class UserRepository : IBaseRepository<User>, IUserRepository
{
    private readonly ApiComparisonDbContext _dbContext;

    public UserRepository(ApiComparisonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    // i still need to think if i will use this or not
    public async Task<Account> GetUserAccountAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Account)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return user!.Account;
    }

    public async Task<Address> GetUserAddressAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Address)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return user!.Address;
    }

    public async Task<IEnumerable<Dish>> GetUserDishesAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Dishes)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return user!.Dishes;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<User> InsertAsync(User entity, CancellationToken cancellationToken)
    {
        User dbEntity = null!;
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            await _dbContext.Accounts.AddAsync(entity.Account);
            await _dbContext.Addresses.AddAsync(entity.Address);

            var userEntry = await _dbContext.Users
                .AddAsync(entity, cancellationToken);
            dbEntity = userEntry.Entity;

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
       catch(Exception ex)
        {
            // logging aici si throw de exceptie
        }

        return dbEntity;
    }

    public async Task UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        _dbContext.Attach(entity.Account);
        _dbContext.Attach(entity.Address);

        if (await _dbContext.Users.FindAsync(entity.Id, cancellationToken) is User found)
        {
            _dbContext.Entry(found).CurrentValues.SetValues(entity);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(User entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = _dbContext.Users;

        if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }

        dbSet.Remove(entityToDelete!);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // this is implemented due to the fact of parallelization of queries when using graphql
    // we need to dispose the dbcontext when the DI container disposes the repository
    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}