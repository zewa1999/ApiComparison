using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.AccountDtos;

public record AccountResponseDto : BaseResponseDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
}