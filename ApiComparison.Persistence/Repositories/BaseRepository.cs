using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ApiComparison.EfCore.Persistence.Repositories;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly ApiComparisonDbContext DbContext;

    public BaseRepository(ApiComparisonDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)!;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>().AsNoTracking()
            .ToListAsync(cancellationToken);
    }


    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<TEntity>()
            .AddAsync(entity, cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity item, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Attach(item);

        DbContext.Entry(item).State= EntityState.Modified;

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbSet = DbContext.Set<TEntity>();

        var entity = await this.GetByIdAsync(id, cancellationToken);

        if (entity != null && DbContext.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }

        dbSet.Remove(entity!);

       await DbContext.SaveChangesAsync(cancellationToken);

    }
}
