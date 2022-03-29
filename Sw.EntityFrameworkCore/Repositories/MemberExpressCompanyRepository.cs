using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class MemberExpressCompanyRepository : BaseRepository<MemberExpressCompany>, IMemberExpressCompanyRepository
    {
        public MemberExpressCompanyRepository(ITransaction transaction) : base(transaction) { }
    }
}
