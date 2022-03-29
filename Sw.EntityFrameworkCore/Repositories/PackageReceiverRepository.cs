using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class PackageReceiverRepository : BaseRepository<PackageReceiver>, IPackageReceiverRepository
    {
        public PackageReceiverRepository(ITransaction transaction) : base(transaction) { }
    }
}
