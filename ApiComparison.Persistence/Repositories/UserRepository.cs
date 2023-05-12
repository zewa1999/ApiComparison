using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
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
    public async Task<Account?> GetUserAccount(Guid? entityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Account)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return user.Account;
    }

    public async Task<Address?> GetUserAddress(Guid? entityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.Account)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return user!.Address;
    }

    public async Task<IEnumerable<Dish>> GetUserDishes(Guid? entityId, CancellationToken cancellationToken)
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
        await _dbContext.Users
            .AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(User incoming, CancellationToken cancellationToken)
    {
        if (await _dbContext.Users.FindAsync(incoming.Id, cancellationToken) is User found)
        {
            _dbContext.Entry(found).CurrentValues.SetValues(incoming);
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