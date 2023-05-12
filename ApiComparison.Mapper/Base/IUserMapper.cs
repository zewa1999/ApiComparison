using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Base;

public interface IUserMapper : IMapper<User, UserRequestDto, UserResponseDto>
{
    User RequestToEntity(UserCreateRequestDto requestDto);

}
