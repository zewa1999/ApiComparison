using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Repositories;

public interface IDishRepository : IBaseRepository<Dish>
{
    public Task<IEnumerable<Ingredient>> GetDishIngredientsAsync(Guid? entityId, CancellationToken cancellationToken);
}