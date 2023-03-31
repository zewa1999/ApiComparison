using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
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
    protected readonly IBaseMapper<TEntity, TRequestDto, TResponseDto> Mapper;

    public BaseService(IBaseRepository<TEntity> repository, IValidator<TRequestDto> validator, IBaseMapper<TEntity, TRequestDto, TResponseDto> mapper)
    {
        Repository = repository;
        Validator = validator;
        Mapper = mapper;
    }

    public async Task<TResponseDto?> GetByID(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await Repository.GetByIdAsync(entityId, cancellationToken);
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
        return Mapper.EntityToResponse(await Repository.InsertAsync(Mapper.RequestToEntity(requestDto), cancellationToken));
    }

    public async Task Update(TRequestDto entityRequestDto, CancellationToken cancellationToken)
    {
        await Repository.UpdateAsync(Mapper.RequestToEntity(entityRequestDto), cancellationToken);
    }

    public async Task DeleteById(TRequestDto entity, CancellationToken cancellationToken)
    {
        await Repository.DeleteEntity(Mapper.RequestToEntity(entity), cancellationToken);
    }
}
