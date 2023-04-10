namespace ApiComparison.Contracts.RequestDto;

public record UserRequestDto : BaseRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public required Guid AccountId { get; set; }
    public required Guid AddressId { get; set; }
}