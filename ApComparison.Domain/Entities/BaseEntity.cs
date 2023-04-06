namespace ApiComparison.Domain.Entities;

public record BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}