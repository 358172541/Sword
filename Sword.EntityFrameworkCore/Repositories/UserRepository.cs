using Sword.Core;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Repositories
{
    public class UserRepository : SwordRepository<User>, IUserRepository
    {
        public UserRepository(ITransaction transaction) : base(transaction) { }
    }
}
