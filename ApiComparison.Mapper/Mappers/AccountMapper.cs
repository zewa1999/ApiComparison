using ApiComparison.Contracts.AccountDtos;
using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;
using System.Threading;

namespace ApiComparison.Mapping.Mappers;

internal class AccountMapper : IAccountMapper
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
            Id = account.Id,
            Username = account.Username,
            Email = account.Email
        };
    }

    public AccountRequestDto UserRequestToAccountRequest(UserCreateRequestDto requestDto)
    {
        return new AccountRequestDto
        {
            Email = requestDto.Email,
            Password = requestDto.Password,
            Username = requestDto.Username
        };
    }
}