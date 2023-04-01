using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Mappers;

public interface IMapper<TEntity, TRequestDto, TResponseDto>
    where TEntity : BaseEntity
    where TRequestDto : BaseRequestDto
    where TResponseDto : BaseResponseDto
{
    TEntity RequestToEntity(TRequestDto requestDto);
    TResponseDto EntityToResponse(TEntity requestDto);
}
