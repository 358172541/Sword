using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class PackageTrackingRepository : BaseRepository<PackageTracking>, IPackageTrackingRepository
    {
        public PackageTrackingRepository(ITransaction transaction) : base(transaction) { }
    }
}
