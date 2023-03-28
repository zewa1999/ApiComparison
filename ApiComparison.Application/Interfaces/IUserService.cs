using ApiComparison.Contracts.RequestDto;
using ApiComparison.Contracts.ResponseDto;

namespace ApiComparison.Application.Interfaces;

public interface IUserService : IBaseService<UserRequestDto, UserResponseDto>
{
}
