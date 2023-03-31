namespace ApiComparison.Contracts.Dto.RequestDto;

public record AccountRequestDto : BaseRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
