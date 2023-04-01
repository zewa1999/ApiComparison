using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IBaseService<TRequestDto, TResponseDto>
    where TRequestDto: BaseRequestDto
    where TResponseDto : BaseResponseDto
{
    Task<TResponseDto> Insert(TRequestDto entity, CancellationToken cancellationToken);

    Task Update(TRequestDto entity, CancellationToken cancellationToken);

    Task DeleteById(Guid entityId, CancellationToken cancellationToken);

    Task<TResponseDto?> GetByID(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<TResponseDto>> GetAll(CancellationToken cancellationToken);
}
