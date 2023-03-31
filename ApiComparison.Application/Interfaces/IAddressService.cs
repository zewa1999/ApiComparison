using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IAddressService : IBaseService<AddressRequestDto, AddressResponseDto>
{
}
