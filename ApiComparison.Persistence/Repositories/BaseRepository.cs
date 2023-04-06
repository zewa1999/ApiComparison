﻿using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly ApiComparisonDbContext DbContext;

    public BaseRepository(ApiComparisonDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(object entityId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>().FindAsync(entityId, cancellationToken);
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
    public async Task UpdateAsync(Guid entityId, TEntity incoming, CancellationToken cancellationToken)
    {
        if (await DbContext.Set<TEntity>().FindAsync(entityId, cancellationToken) is TEntity found)
        {
            found.LastUpdatedAt = DateTime.UtcNow;
            DbContext.Entry(found).CurrentValues.SetValues(incoming);
        }

        DbContext.Entry(incoming).State = EntityState.Modified;

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteEntity(TEntity entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = DbContext.Set<TEntity>();

        if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }

        dbSet.Remove(entityToDelete!);

        await DbContext.SaveChangesAsync(cancellationToken);
    }
}