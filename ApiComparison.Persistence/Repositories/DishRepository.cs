using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class DishRepository : BaseRepository<Dish>, IDishRepository
{
    public DishRepository(ApiComparisonDbContext dbContext) : base(dbContext)
    {
    }
}
