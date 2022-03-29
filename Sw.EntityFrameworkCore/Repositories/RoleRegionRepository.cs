using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class RoleRegionRepository : BaseRepository<RoleRegion>, IRoleRegionRepository
    {
        public RoleRegionRepository(ITransaction transaction) : base(transaction) { }
    }
}
