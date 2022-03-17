using Core;
using Domain;

namespace EntityFrameworkCore.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ITransaction transaction) : base(transaction) { }
    }
}
