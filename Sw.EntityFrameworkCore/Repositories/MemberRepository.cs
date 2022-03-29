using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(ITransaction transaction) : base(transaction) { }
    }
}
