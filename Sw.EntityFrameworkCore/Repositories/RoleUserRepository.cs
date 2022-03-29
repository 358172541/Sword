using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class RoleUserRepository : BaseRepository<RoleUser>, IRoleUserRepository
    {
        public RoleUserRepository(ITransaction transaction) : base(transaction) { }
    }
}
