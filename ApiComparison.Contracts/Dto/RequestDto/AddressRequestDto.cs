namespace ApiComparison.Contracts.RequestDto;

public record AddressRequestDto : BaseRequestDto
{
    public required string Street { get; set; } = null!;
    public required int StreetNumber { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public required UserRequestDto UserRequestDto { get; set; }
}
