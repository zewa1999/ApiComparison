using ApiComparison.Application.Interfaces;
using ApiComparison.Contracts.Dto.RequestDto;
using ApiComparison.Contracts.Dto.ResponseDto;
using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using ApiComparison.Mapping.Mappers;
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
