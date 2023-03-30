namespace ApiComparison.Contracts.RequestDto;

public record AddressRequestDto : BaseRequestDto
{
    public required string Street { get; set; } = null!;
    public required string StreetNumber { get; set; }
    public required string City { get; set; }
    public required Guid UserId { get; set; }
}
