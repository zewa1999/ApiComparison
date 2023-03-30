using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapper;
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

    //protected readonly Validator Validator; this needs to be added
    public BaseService(IBaseRepository<TEntity> repository, IValidator<TRequestDto> validator, IBaseMapper<TEntity, TRequestDto, TResponseDto> mapper)
    {
        Repository = repository;
        Validator = validator;
        Mapper = mapper;
    }

    public async Task<TResponseDto> GetByID(Guid id, CancellationToken cancellationToken)
    {
        return await Repository.GetByIdAsync(id, cancellationToken);
    }

    public IEnumerable<TResponseDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<TResponseDto> Insert(TRequestDto entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(TRequestDto entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(TRequestDto entity)
    {
        throw new NotImplementedException();
    }

}
