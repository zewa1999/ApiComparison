using ApiComparison.Contracts.BaseDtos;

namespace ApiComparison.Contracts.AddressDtos;

public record AddressRequestDto : BaseRequestDto
{
    public required string Street { get; set; } = null!;
    public required string StreetNumber { get; set; }
    public required string City { get; set; }
}