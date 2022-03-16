using Sword.Core;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Repositories
{
    public class RoleRescRepository : SwordRepository<RoleResc>, IRoleRescRepository
    {
        public RoleRescRepository(ITransaction transaction) : base(transaction) { }
    }
}
