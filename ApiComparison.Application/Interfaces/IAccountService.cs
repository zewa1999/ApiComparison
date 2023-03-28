using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces;

public interface IAccountService : IBaseService<AccountRequestDto, AccountResponseDto>
{
}
