using ApiComparison.Contracts.AccountDtos;
using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Base;

public interface IAccountMapper : IMapper<Account, AccountRequestDto, AccountResponseDto>
{
    public AccountRequestDto UserRequestToAccountRequest(UserCreateRequestDto requestDto);
}
