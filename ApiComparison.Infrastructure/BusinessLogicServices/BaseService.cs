using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Contracts.Extensions;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Exceptions;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapping.Mappers;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class BaseService<TEntity, TRequestDto, TResponseDto> : IBaseService<TRequestDto, TResponseDto>
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
    where TEntity : BaseEntity
{
    protected readonly IBaseRepository<TEntity> Repository;
    protected readonly IValidator<TRequestDto> Validator;
    protected readonly IMapper<TEntity, TRequestDto, TResponseDto> Mapper;

    public BaseService(IBaseRepository<TEntity> repository, IValidator<TRequestDto> validator, IMapper<TEntity, TRequestDto, TResponseDto> mapper)
    {
        Repository = repository;
        Validator = validator;
        Mapper = mapper;
    }

    public async Task<TResponseDto?> GetByID(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await Repository.GetByIdAsync(entityId, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity));
        }
        return Mapper.EntityToResponse(entity!);
    }

    public async Task<IEnumerable<TResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await Repository.GetAllAsync(cancellationToken);
        var responses = new List<TResponseDto>();

        foreach (var entity in entities)
        {
            responses.Add(Mapper.EntityToResponse(entity));
        }

        return responses;
    }

    public async Task<TResponseDto> Insert(TRequestDto requestDto, CancellationToken cancellationToken)
    {
        Validator.ValidateAndThrowAggregateException(requestDto);
        return Mapper.EntityToResponse(await Repository.InsertAsync(Mapper.RequestToEntity(requestDto), cancellationToken));
    }

    public async Task Update(TRequestDto requestDto, CancellationToken cancellationToken)
    {
        Validator.ValidateAndThrowAggregateException(requestDto);
        await Repository.UpdateAsync(Mapper.RequestToEntity(requestDto), cancellationToken);
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
