using Core;
using Domain;

namespace EntityFrameworkCore.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ITransaction transaction) : base(transaction) { }
    }
}
