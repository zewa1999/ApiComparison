using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IAsyncDisposable
    where TEntity : BaseEntity
{
    private readonly ApiComparisonDbContext DbContext;

    public BaseRepository(ApiComparisonDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>().AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    // remove the dependency to DateTime.UtcNow(create interface for mocking)
    public async Task UpdateAsync(TEntity incoming, CancellationToken cancellationToken)
    {
        if (await DbContext.Set<TEntity>().FindAsync(incoming.Id, cancellationToken) is TEntity found)
        {
            found.LastUpdatedAt = DateTime.UtcNow;
            DbContext.Entry(found).CurrentValues.SetValues(incoming);
        }

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(TEntity entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = DbContext.Set<TEntity>();

        if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }

        dbSet.Remove(entityToDelete!);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    // this is implemented due to the fact of parallelization of queries when using graphql
    // we need to dispose the dbcontext when the DI container disposes the repository
    public ValueTask DisposeAsync()
    {
        return DbContext.DisposeAsync();
    }
}