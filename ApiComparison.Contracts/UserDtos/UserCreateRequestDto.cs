using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.UserDtos;

public record UserCreateRequestDto : BaseRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Street { get; set; } = null!;
    public required string StreetNumber { get; set; }
    public required string City { get; set; }
}