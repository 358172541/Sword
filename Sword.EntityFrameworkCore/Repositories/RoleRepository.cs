using Sword.Core;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Repositories
{
    public class RoleRepository : SwordRepository<Role>, IRoleRepository
    {
        public RoleRepository(ITransaction transaction) : base(transaction) { }
    }
}
