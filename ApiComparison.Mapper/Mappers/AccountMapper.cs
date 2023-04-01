using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Mappers;

public class AccountMapper : IMapper<Account, AccountRequestDto, AccountResponseDto>
{
    public Account RequestToEntity(AccountRequestDto requestDto)
    {
        return new Account
        {
            Username = requestDto.Username,
            Password = requestDto.Password,
            Email = requestDto.Email
        };
    }

    public AccountResponseDto EntityToResponse(Account account)
    {
        return new AccountResponseDto
        {
            Username = account.Username,
            Password = account.Password,
            Email = account.Email
        };
    }
}
