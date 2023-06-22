using ApiComparison.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiComparison.EfCore.Persistence.Configurations;

internal class DishConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(d => d.PhotoUrl)
            .IsRequired()
            .HasMaxLength(120);

        builder.HasMany(d => d.Ingredients)
            .WithMany(d => d.Dishes);
    }
}