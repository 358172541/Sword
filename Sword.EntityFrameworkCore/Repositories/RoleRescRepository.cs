using Core;
using Domain;

namespace EntityFrameworkCore.Repositories
{
    public class RoleRescRepository : BaseRepository<RoleResc>, IRoleRescRepository
    {
        public RoleRescRepository(ITransaction transaction) : base(transaction) { }
    }
}
