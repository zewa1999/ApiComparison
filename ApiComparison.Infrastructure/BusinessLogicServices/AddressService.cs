using ApiComparison.Application.Interfaces.BusinessServices;
using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.Exceptions;
using ApiComparison.Validation.Extensions;
using FluentValidation;
using ApiComparison.Domain.Repositories;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AddressService : IBaseService<Address>, IAddressService
{
    protected readonly IAddressRepository _repository;
    protected readonly IValidator<Address> _validator;

    public AddressService(IAddressRepository repository, IValidator<Address> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Address> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Address));
        }

        return entity;
    }

    public async Task<IEnumerable<Address>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Address> InsertAsync(Address entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);
        return await _repository.InsertAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(Guid entityId, Address entity, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrowAggregateException(entity);

        var dbEntity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (dbEntity == null)
        {
            throw new EntityNotFoundException(typeof(Address));
        }

        entity.Id = dbEntity.Id;
        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Address));
        }

        await _repository.Delete(entity, cancellationToken);
    }
}