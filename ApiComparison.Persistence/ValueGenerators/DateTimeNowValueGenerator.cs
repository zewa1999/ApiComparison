using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ApiComparison.EfCore.Persistence.ValueGenerators;

internal class DateTimeNowValueGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;
}