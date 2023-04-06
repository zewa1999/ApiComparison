namespace ApiComparison.Contracts.RequestDto;

public record UserRequestDto : BaseRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public AccountRequestDto AccountRequestDto { get; set; } = null!;
    public required AddressRequestDto AddressRequestDto { get; set; }
}