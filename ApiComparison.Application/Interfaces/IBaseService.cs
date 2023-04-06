using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces;

public interface IBaseService<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken);

    Task Update(Guid entityId, TEntity entity, CancellationToken cancellationToken);

    Task DeleteById(Guid entityId, CancellationToken cancellationToken);

    Task<TEntity?> GetByID(Guid? id, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
}