using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(ApiComparisonDbContext dbContext) : base(dbContext)
    {
    }
}