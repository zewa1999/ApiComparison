using ApiComparison.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiComparison.EfCore.Persistence.Configurations;

internal class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(i => i.Quantity)
            .IsRequired();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasMany(i => i.Dishes)
           .WithMany(i => i.Ingredients);
    }
}