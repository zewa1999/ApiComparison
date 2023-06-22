using ApiComparison.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiComparison.EfCore.Persistence.Configurations;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(50);
    }
}