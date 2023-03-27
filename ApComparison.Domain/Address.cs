namespace ApiComparison.Domain;

public class Address : BaseEntity
{
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
}
