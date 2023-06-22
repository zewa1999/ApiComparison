using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Exceptions;
using ApiComparison.Validation.Extensions;
using FluentValidation;
using ApiComparison.Domain.Repositories;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class UserService : IBaseService<User>, IUserService
{
    protected readonly IUserRepository _repository;
    protected readonly IValidator<User> _validator;

    public UserService(IUserRepository repository, IValidator<User> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<User> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        return entity;
    }

    public async Task<IEnumerable<Dish>> GetUserDishes(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        return await _repository.GetUserDishesAsync(entityId, cancellationToken);
    }

    public async Task<Account> GetUserAccount(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        return await _repository.GetUserAccountAsync(entityId, cancellationToken);
    }

    public async Task<Address> GetUserAddress(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        return await _repository.GetUserAddressAsync(entityId, cancellationToken);
    }

    public async Task<IEnumerable<Dish>> GetDishIngredients(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        return await _repository.GetUserDishesAsync(entityId, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<User> InsertAsync(User entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);
        return await _repository.InsertAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(Guid entityId, User entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);

        var dbEntity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (dbEntity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        entity.Id = entityId;
        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(User));
        }

        await _repository.Delete(entity, cancellationToken);
    }
}