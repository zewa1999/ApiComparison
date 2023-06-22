using ApiComparison.Domain.Entities;
using ApiComparison.EfCore.Persistence.ValueGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace ApiComparison.EfCore.Persistence;

public class ApiComparisonDbContext : DbContext
{
    public ApiComparisonDbContext()
    {

    }

    public ApiComparisonDbContext(DbContextOptions<ApiComparisonDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (IMutableEntityType mutableEntityType in builder.Model.GetEntityTypes())
        {
            if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseEntity)))
            {
                IMutableProperty createdAtProperty = mutableEntityType.GetProperty(nameof(BaseEntity.CreatedAt));
                createdAtProperty.ValueGenerated = ValueGenerated.OnAdd;
                createdAtProperty.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                createdAtProperty.SetDefaultValueSql("NOW()");

                IMutableProperty updatedAtProperty =
                    mutableEntityType.GetProperty(nameof(BaseEntity.LastUpdatedAt));
                updatedAtProperty.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                updatedAtProperty.SetAfterSaveBehavior(PropertySaveBehavior.Save);
                updatedAtProperty.SetValueGeneratorFactory((_, _) => new DateTimeNowValueGenerator());
                updatedAtProperty.SetDefaultValueSql("NOW()");
            }
        }

        base.OnModelCreating(builder);
    }

    public virtual DbSet<Account> Accounts { get; set; } = null!;
    public virtual DbSet<Address> Addresses { get; set; } = null!;
    public virtual DbSet<Dish> Dishes { get; set; } = null!;
    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
}