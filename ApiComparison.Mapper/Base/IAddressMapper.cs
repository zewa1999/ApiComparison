using ApiComparison.Contracts.AddressDtos;
using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Base;

public interface IAddressMapper : IMapper<Address, AddressRequestDto, AddressResponseDto>
{
    public AddressRequestDto UserRequestToAddressRequest(UserCreateRequestDto requestDto);
}
