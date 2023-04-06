using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Mappers;

internal class AddressUserMapper : IMapper<Address, AddressRequestDto, AddressResponseDto>, IMapper<User, UserRequestDto, UserResponseDto>
{
    private readonly IMapper<Account, AccountRequestDto, AccountResponseDto> _accountMapper;

    public AddressUserMapper(IMapper<Account, AccountRequestDto, AccountResponseDto> accountMapper)
    {
        _accountMapper = accountMapper;
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
            UserResponseDto = EntityToResponse(address.User)
        };
    }

    public UserResponseDto EntityToResponse(User requestDto)
    {
        return new UserResponseDto
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Bio = requestDto.Bio,
            AccountResponseDto = _accountMapper.EntityToResponse(requestDto.Account),
            AddressResponseDto = EntityToResponse(requestDto.Address)
        };
    }

    public User RequestToEntity(UserRequestDto requestDto)
    {
        return new User
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Bio = requestDto.Bio,
            Account = _accountMapper.RequestToEntity(requestDto.AccountRequestDto),
            Address = RequestToEntity(requestDto.AddressRequestDto)
        };
    }
}