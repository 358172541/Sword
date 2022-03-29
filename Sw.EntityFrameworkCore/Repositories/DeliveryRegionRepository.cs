using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class DeliveryRegionRepository : BaseRepository<DeliveryRegion>, IDeliveryRegionRepository
    {
        public DeliveryRegionRepository(ITransaction transaction) : base(transaction) { }
    }
}
