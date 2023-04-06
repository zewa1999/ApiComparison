using ApiComparison.Domain.Entities;
using ApiComparison.Domain.Interfaces.Repositories;

namespace ApiComparison.EfCore.Persistence.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(ApiComparisonDbContext dbContext) : base(dbContext)
    {
    }
}