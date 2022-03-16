using Sword.Core;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Repositories
{
    public class UserRoleRepository : SwordRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ITransaction transaction) : base(transaction) { }
    }
}
