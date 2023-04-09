namespace ApiComparison.Domain.Entities;

public record User : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Bio { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public Guid AddressId { get; set; }
    public required Address Address { get; set; }
}