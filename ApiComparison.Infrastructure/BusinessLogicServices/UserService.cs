using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapper;
using FluentValidation;

namespace ApiComparison.Infrastructure.BusinessLogicServices;

public class UserService : BaseService<User, UserRequestDto, UserResponseDto>, IUserService
{
    public UserService(IBaseRepository<User> repository,
                       IValidator<UserRequestDto> validator,
                       IBaseMapper<User, UserRequestDto, UserResponseDto> mapper) : base(repository, validator, mapper)
    {
    }
}
