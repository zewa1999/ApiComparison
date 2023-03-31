using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Application.Interfaces;

public interface IAccountService : IBaseService<AccountRequestDto, AccountResponseDto>
{
}
