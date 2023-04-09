namespace ApiComparison.Domain.Entities;

public record Address : BaseEntity
{
    public required string Street { get; set; } = null!;
    public required string StreetNumber { get; set; }
    public required string City { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}