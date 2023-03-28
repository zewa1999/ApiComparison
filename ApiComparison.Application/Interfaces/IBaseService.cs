using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IBaseService<TRequestDto, TResponseDto>
    where TRequestDto: BaseRequestDto
    where TResponseDto : BaseResponseDto
{
    Task<TResponseDto> Insert(TRequestDto entity);

    Task Update(TRequestDto entity);

    void DeleteById(TRequestDto entity);

    TResponseDto GetByID(Guid id);

    IEnumerable<TResponseDto> GetAll();
}
