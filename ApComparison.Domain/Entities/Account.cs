namespace ApiComparison.Domain.Entities;

public record Account : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}