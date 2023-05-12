using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class DishRepository : IBaseRepository<Dish>, IDishRepository
{
    private readonly ApiComparisonDbContext _dbContext;

    public DishRepository(ApiComparisonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dish?> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Dishes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    public async Task<IEnumerable<Ingredient>> GetDishIngredients(Guid? entityId, CancellationToken cancellationToken)
    {
        var dish = await _dbContext.Dishes
            .Include(d => d.Ingredients)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return dish!.Ingredients;
    }

    public async Task<IEnumerable<Dish>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Dishes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Dish> InsertAsync(Dish entity, CancellationToken cancellationToken)
    {
        await _dbContext.Dishes
            .AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(Dish incoming, CancellationToken cancellationToken)
    {
        if (await _dbContext.Dishes.FindAsync(incoming.Id, cancellationToken) is Dish found)
        {
            _dbContext.Entry(found).CurrentValues.SetValues(incoming);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Dish entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = _dbContext.Dishes;

        if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }

        dbSet.Remove(entityToDelete!);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // this is implemented due to the fact of parallelization of queries when using graphql
    // we need to dispose the dbcontext when the DI container disposes the repository
    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}