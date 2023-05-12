using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Exceptions;
using ApiComparison.Domain.Interfaces.Repositories;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class DishService : BaseService<Dish>, IDishService
{
    public DishService(IDishRepository repository,
                       IValidator<Dish> validator) : base(repository, validator)
    {
    }

    public async Task<IEnumerable<Ingredient>> GetDishIngredients(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await Repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Dish));
        }

        return await Repository.Ing
    }
}