namespace ApiComparison.Contracts.ResponseDto;

public record AccountResponseDto : BaseResponseDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}