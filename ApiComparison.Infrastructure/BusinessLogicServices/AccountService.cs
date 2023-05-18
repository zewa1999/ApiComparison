using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Exceptions;
using ApiComparison.Validation.Extensions;
using FluentValidation;
using ApiComparison.Domain.Repositories;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AccountService : IBaseService<Account>, IAccountService
{
    protected readonly IAccountRepository _repository;
    protected readonly IValidator<Account> _validator;

    public AccountService(IAccountRepository repository, IValidator<Account> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Account> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Account));
        }

        return entity;
    }

    public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Account> InsertAsync(Account entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);
        return await _repository.InsertAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(Guid entityId, Account entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);

        var dbEntity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (dbEntity == null)
        {
            throw new EntityNotFoundException(typeof(Account));
        }

        entity.Id = dbEntity.Id;
        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Account));
        }

        await _repository.Delete(entity, cancellationToken);
    }
}