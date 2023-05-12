using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.AccountDtos;

public record AccountRequestDto : BaseRequestDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}