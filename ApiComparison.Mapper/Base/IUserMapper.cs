using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;

namespace ApiComparison.Mapping.Base;

public interface IUserMapper : IMapper<User, UserPutRequestDto, UserResponseDto>
{
    User RequestToEntity(UserCreateRequestDto requestDto);

}
