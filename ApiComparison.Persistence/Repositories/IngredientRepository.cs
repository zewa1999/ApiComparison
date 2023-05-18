using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class IngredientRepository : IBaseRepository<Ingredient>, IIngredientRepository
{
    private readonly ApiComparisonDbContext _dbContext;

    public IngredientRepository(ApiComparisonDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Ingredient?> GetByIdAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Ingredients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
    }

    public async Task<IEnumerable<Dish>> GetIngredientDishesAsync(Guid? entityId, CancellationToken cancellationToken)
    {
        var ingredient = await _dbContext.Ingredients
            .Include(d => d.Dishes)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        return ingredient!.Dishes;
    }

    public async Task<IEnumerable<Ingredient>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Ingredients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Ingredient> InsertAsync(Ingredient entity, CancellationToken cancellationToken)
    {
        var dbEntry = await _dbContext.Ingredients
            .AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return dbEntry.Entity;
    }

    public async Task UpdateAsync(Ingredient incoming, CancellationToken cancellationToken)
    {
        if (await _dbContext.Ingredients.FindAsync(incoming.Id, cancellationToken) is Ingredient found)
        {
            _dbContext.Entry(found).CurrentValues.SetValues(incoming);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Ingredient entityToDelete, CancellationToken cancellationToken)
    {
        var dbSet = _dbContext.Ingredients;

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