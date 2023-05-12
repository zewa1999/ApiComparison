using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces.BusinessServices;

public interface IBaseService<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);

    Task UpdateAsync(Guid entityId, TEntity entity, CancellationToken cancellationToken);

    Task DeleteByIdAsync(Guid entityId, CancellationToken cancellationToken);

    Task<TEntity> GetByIdAsync(Guid? id, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}