using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IBaseService<TRequestDto, TResponseDto>
    where TRequestDto: BaseRequestDto
    where TResponseDto : BaseResponseDto
{
    Task<TResponseDto> Insert(TRequestDto entity, CancellationToken cancellationToken);

    Task Update(TRequestDto entity, CancellationToken cancellationToken);

    Task DeleteById(TRequestDto entity, CancellationToken cancellationToken);

    Task<TResponseDto?> GetByID(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<TResponseDto>> GetAll(CancellationToken cancellationToken);
}
