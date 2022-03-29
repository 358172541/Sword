using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class PackageNumberRepository : BaseRepository<PackageNumber>, IPackageNumberRepository
    {
        public PackageNumberRepository(ITransaction transaction) : base(transaction) { }
    }
}
