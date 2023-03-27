namespace ApiComparison.Domain.Entities;

public class Account : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
