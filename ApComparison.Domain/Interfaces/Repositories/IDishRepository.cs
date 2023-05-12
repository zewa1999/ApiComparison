using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Interfaces.Repositories;

public interface IDishRepository : IBaseRepository<Dish>
{
    public Task<IEnumerable<Ingredient>> GetDishIngredients(Guid? entityId, CancellationToken cancellationToken)
}