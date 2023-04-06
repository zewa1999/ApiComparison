using ApiComparison.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiComparison.EfCore.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(40);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(40);

        builder.Property(u => u.Bio)
              .IsRequired()
              .HasMaxLength(120);

        builder.HasOne(u => u.Account)
            .WithOne()
            .HasForeignKey<User>(a => a.AccountId);

        builder.HasOne(u => u.Address)
            .WithOne()
            .HasForeignKey<User>(a => a.AddressId);
    }
}