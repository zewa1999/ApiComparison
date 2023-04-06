using ApiComparison.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiComparison.EfCore.Persistence;

public class ApiComparisonDbContext : DbContext
{
    public ApiComparisonDbContext(DbContextOptions<ApiComparisonDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Account> Addresses { get; set; } = null!;
    public DbSet<Account> Dishes { get; set; } = null!;
    public DbSet<Account> Ingredients { get; set; } = null!;
    public DbSet<Account> Users { get; set; } = null!;
}