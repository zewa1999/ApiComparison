using ApiComparison.Domain.Entities;

namespace ApiComparison.Domain.Repositories;

public interface IIngredientRepository : IBaseRepository<Ingredient>
{
    public Task<IEnumerable<Dish>> GetIngredientDishesAsync(Guid? entityId, CancellationToken cancellationToken);
}