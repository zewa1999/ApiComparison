using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Exceptions;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Validation.Extensions;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class IngredientService : IBaseService<Ingredient>, IIngredientService
{
    protected readonly IIngredientRepository _repository;
    protected readonly IValidator<Ingredient> _validator;

    public IngredientService(IIngredientRepository repository, IValidator<Ingredient> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Ingredient> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Ingredient));
        }

        return entity;
    }

    public async Task<IEnumerable<Dish>> GetDishesOfIngredient(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Ingredient));
        }

        return await _repository.GetIngredientDishesAsync(entityId, cancellationToken);
    }

    public async Task<IEnumerable<Ingredient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Ingredient> InsertAsync(Ingredient entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);
        return await _repository.InsertAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(Guid entityId, Ingredient entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);

        var dbEntity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (dbEntity == null)
        {
            throw new EntityNotFoundException(typeof(Ingredient));
        }

        entity.Id = dbEntity.Id;
        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Ingredient));
        }

        await _repository.Delete(entity, cancellationToken);
    }
}