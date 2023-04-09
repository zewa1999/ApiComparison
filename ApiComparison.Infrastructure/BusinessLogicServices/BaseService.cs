using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Exceptions;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Validation.Extensions;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class BaseService<TEntity, TRequestDto, TResponseDto> : IBaseService<TEntity>
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
    where TEntity : BaseEntity
{
    protected readonly IBaseRepository<TEntity> Repository;
    protected readonly IValidator<TEntity> Validator;

    public BaseService(IBaseRepository<TEntity> repository, IValidator<TEntity> validator)
    {
        Repository = repository;
        Validator = validator;
    }

    public async Task<TEntity> GetByID(Guid? entityId, CancellationToken cancellationToken)
    {
        var entity = await Repository.GetByIdAsync(entityId!, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity));
        }

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await Repository.GetAllAsync(cancellationToken);
    }

    public async Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken)
    {
        Validator.ValidateAndThrowAggregateException(entity);
        return await Repository.InsertAsync(entity, cancellationToken);
    }

    public async Task Update(Guid entityId, TEntity entity, CancellationToken cancellationToken)
    {
        Validator.ValidateAndThrowAggregateException(entity);

        var dbEntity = await Repository.GetByIdAsync(entityId, cancellationToken);

        if (dbEntity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity));
        }

        entity.Id = dbEntity.Id;
        await Repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteById(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await Repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity));
        }

        await Repository.DeleteEntity(entity, cancellationToken);
    }
}