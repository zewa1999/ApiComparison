using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Mappers;

public class UserMapper : IBaseMapper<User, UserRequestDto, UserResponseDto>
{
    private readonly IBaseMapper<Account, AccountRequestDto, AccountResponseDto> _accountMapper;
    private readonly IBaseMapper<Address, AddressRequestDto, AddressResponseDto> _addressMapper;

    public UserMapper(
        IBaseMapper<Account, AccountRequestDto, AccountResponseDto> accountMapper,
        IBaseMapper<Address, AddressRequestDto, AddressResponseDto> addressMapper)
    {
        _accountMapper = accountMapper;
        _addressMapper = addressMapper;
    }

    public UserResponseDto EntityToResponse(User requestDto)
    {
        return new UserResponseDto
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Bio = requestDto.Bio,
            AccountResponseDto = _accountMapper.EntityToResponse(requestDto.Account),
            AddressResponseDto = _addressMapper.EntityToResponse(requestDto.Address)
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
            Address = _addressMapper.RequestToEntity(requestDto.AddressRequestDto)
        };
    }


}