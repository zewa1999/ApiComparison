using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid? id, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity incoming, CancellationToken cancellationToken);

    Task Delete(TEntity entityToDelete, CancellationToken cancellationToken);
}