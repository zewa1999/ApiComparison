using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapper;

public class AddressMapper : IBaseMapper<Address, AddressRequestDto, AddressResponseDto>
{
    private readonly IBaseMapper<User, UserRequestDto, UserResponseDto> _userMapper;

    public AddressMapper(IBaseMapper<User, UserRequestDto, UserResponseDto> userMapper)
    {
        _userMapper = userMapper;
    }
    public Address RequestToEntity(AddressRequestDto requestDto)
    {
        return new Address
        {
            Street = requestDto.Street,
            StreetNumber = requestDto.StreetNumber,
            City = requestDto.City,
            UserId = requestDto.UserId
        };
    }

    public AddressResponseDto EntityToResponse(Address address)
    {
        return new AddressResponseDto
        {
            Street = address.Street,
            StreetNumber = address.StreetNumber,
            City = address.City,
            UserResponseDto = _userMapper.EntityToResponse(address.User)
        };
    }
}