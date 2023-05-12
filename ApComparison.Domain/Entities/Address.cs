namespace ApiComparison.Domain.Entities;

public record Address : BaseEntity
{
    public required string Street { get; set; } = null!;
    public required string StreetNumber { get; set; }
    public required string City { get; set; }
}