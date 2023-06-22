using ApiComparison.Contracts.UserDtos;
using ApiComparison.Domain.Entities;
using ApiComparison.Mapping.Base;

namespace ApiComparison.Mapping.Mappers;

internal class UserMapper : IUserMapper
{
    public UserResponseDto EntityToResponse(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Bio = user.Bio
        };
    }

    public User RequestToEntity(UserPutRequestDto requestDto)
    {
        return new User
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Bio = requestDto.Bio
        };
    }

    public User RequestToEntity(UserCreateRequestDto requestDto)
    {
        return new User
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Bio = requestDto.Bio,
            Account = new Account
            {
                Email = requestDto.Email,
                Password = requestDto.Password,
                Username = requestDto.Username
            },
            Address = new Address
            {
                City = requestDto.City,
                Street = requestDto.Street,
                StreetNumber = requestDto.StreetNumber,
            }
        };
    }
}