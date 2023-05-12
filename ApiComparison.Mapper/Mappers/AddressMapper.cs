using ApiComparison.Contracts.AddressDtos;
using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using System.Threading;

namespace ApiComparison.Mapping.Mappers;

internal class AddressMapper : IAddressMapper
{
    public Address RequestToEntity(AddressRequestDto requestDto)
    {
        return new Address
        {
            Street = requestDto.Street,
            StreetNumber = requestDto.StreetNumber,
            City = requestDto.City
        };
    }

    public AddressResponseDto EntityToResponse(Address address)
    {
        return new AddressResponseDto
        {
            Id = address.Id,
            Street = address.Street,
            StreetNumber = address.StreetNumber,
            City = address.City
        };
    }

    public AddressRequestDto UserRequestToAddressRequest(UserCreateRequestDto requestDto)
    {
        return new AddressRequestDto
        {
            City = requestDto.City,
            Street = requestDto.Street,
            StreetNumber = requestDto.StreetNumber
        };
    }
}