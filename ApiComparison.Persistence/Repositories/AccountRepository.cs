using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

internal class AccountRepository : IBaseRepository<Account>, IAccountRepository
{
    private readonly ApiComparisonDbContext _dbContext;

    public AccountRepository(ApiComparisonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account?> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Accounts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Account> InsertAsync(Account entity, CancellationToken cancellationToken)
    {
        var dbEntry = await _dbContext.Accounts
            .AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return dbEntry.Entity;
    }

    public async Task UpdateAsync(Account incoming, CancellationToken cancellationToken)
    {
        if (await _dbContext.Accounts.FindAsync(incoming.Id, cancellationToken) is Account found)
        {
            _dbContext.Entry(found).CurrentValues.SetValues(incoming);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Account entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = _dbContext.Accounts;

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