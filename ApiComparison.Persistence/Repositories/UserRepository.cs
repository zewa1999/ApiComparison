using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApiComparisonDbContext dbContext) : base(dbContext)
    {
    }
}
