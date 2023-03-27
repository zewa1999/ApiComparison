namespace ApiComparison.Domain.Entities;

public class Address : BaseEntity
{
    public required string Street { get; set; } = null!;
    public required int StreetNumber { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
}
