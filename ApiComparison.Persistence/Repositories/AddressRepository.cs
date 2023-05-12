using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class AddressRepository : IBaseRepository<Address>, IAddressRepository
{
    private readonly ApiComparisonDbContext _dbContext;

    public AddressRepository(ApiComparisonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Address?> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    public async Task<IEnumerable<Address>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Addresses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Address> InsertAsync(Address entity, CancellationToken cancellationToken)
    {
        var dbEntry = await _dbContext.Addresses
            .AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return dbEntry.Entity;
    }

    public async Task UpdateAsync(Address incoming, CancellationToken cancellationToken)
    {
        if (await _dbContext.Addresses.FindAsync(incoming.Id, cancellationToken) is Address found)
        {
            _dbContext.Entry(found).CurrentValues.SetValues(incoming);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Address entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = _dbContext.Addresses;

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