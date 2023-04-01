using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapping.Mappers;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class AccountService : BaseService<Account, AccountRequestDto, AccountResponseDto>, IAccountService
{
    public AccountService(IAccountRepository repository,
                          IValidator<AccountRequestDto> validator,
                          IMapper<Account, AccountRequestDto, AccountResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
