using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class BaseService<TEntity, TRequestDto, TResponseDto> : IBaseService<TRequestDto, TResponseDto>
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
    where TEntity : BaseEntity
{
    protected readonly IBaseRepository<TEntity> Repository;
    //protected readonly Validator Validator; this needs to be added
    public BaseService(IBaseRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public TResponseDto GetByID(Guid id)
    {
      // if(Validator.ValidateObject()...)  return aggregate exception of the things that are not good
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
