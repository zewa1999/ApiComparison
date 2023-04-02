using ApiComparison.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiComparison.EfCore.Persistence.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.AddressId);

        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Address>(a => a.UserId);

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(a => a.StreetNumber)
            .IsRequired();

        builder.Property(a => a.StreetNumber)
            .IsRequired();

    }
}
