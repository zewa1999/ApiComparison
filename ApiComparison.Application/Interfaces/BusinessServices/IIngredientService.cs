using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces.BusinessServices;

public interface IIngredientService : IBaseService<Ingredient>
{
    public Task<IEnumerable<Dish>> GetDishesOfIngredient(Guid? entityId, CancellationToken cancellationToken);
}