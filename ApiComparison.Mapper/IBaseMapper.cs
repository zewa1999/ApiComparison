using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapper;

public interface IBaseMapper<TEntity, TRequestDto, TResponseDto>
    where TEntity : BaseEntity
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
{
    TEntity RequestToEntity(TRequestDto requestDto); 
    TResponseDto EntityToResponse(TEntity requestDto); 
}
