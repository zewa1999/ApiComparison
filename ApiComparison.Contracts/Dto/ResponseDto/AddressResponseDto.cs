namespace ApiComparison.Contracts.ResponseDto;

public record  AddressResponseDto : BaseResponseDto
{
    public required string Street { get; set; } = null!;
    public required int StreetNumber { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public Guid UserId { get; set; }
}
