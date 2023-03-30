namespace ApiComparison.Contracts.ResponseDto;

public record  AddressResponseDto : BaseResponseDto
{
    public required string Street { get; set; } = null!;
    public required string StreetNumber { get; set; }
    public required string City { get; set; }
    public required UserResponseDto UserResponseDto { get; set; }
}
