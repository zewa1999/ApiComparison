using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity item, CancellationToken cancellationToken);
    Task DeleteEntity(TEntity toDeleteEntity, CancellationToken cancellationToken);
}
