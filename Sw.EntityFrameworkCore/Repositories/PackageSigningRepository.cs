using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class PackageSigningRepository : BaseRepository<PackageSigning>, IPackageSigningRepository
    {
        public PackageSigningRepository(ITransaction transaction) : base(transaction) { }
    }
}
