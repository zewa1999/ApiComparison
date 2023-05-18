using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Exceptions;
using ApiComparison.Validation.Extensions;
using FluentValidation;
using ApiComparison.Domain.Repositories;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class DishService : IBaseService<Dish>, IDishService
{
    protected readonly IDishRepository _repository;
    protected readonly IValidator<Dish> _validator;

    public DishService(IDishRepository repository, IValidator<Dish> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Dish> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Dish));
        }

        return entity;
    }

    public async Task<IEnumerable<Ingredient>> GetIngredientsOfDish(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Dish));
        }

        return await _repository.GetDishIngredientsAsync(entityId, cancellationToken);
    }

    public async Task<IEnumerable<Dish>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Dish> InsertAsync(Dish entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);
        return await _repository.InsertAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(Guid entityId, Dish entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);

        var dbEntity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (dbEntity == null)
        {
            throw new EntityNotFoundException(typeof(Dish));
        }

        entity.Id = dbEntity.Id;
        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Dish));
        }

        await _repository.Delete(entity, cancellationToken);
    }
}