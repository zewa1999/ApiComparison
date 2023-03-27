using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    TEntity GetByID(Guid id);
    IEnumerable<TEntity> GetAll();
    bool Insert(TEntity entity);
    bool Update(TEntity item);
    bool DeleteById(Guid id);
}
