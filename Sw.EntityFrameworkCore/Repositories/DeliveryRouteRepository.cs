using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class DeliveryRouteRepository : BaseRepository<DeliveryRoute>, IDeliveryRouteRepository
    {
        public DeliveryRouteRepository(ITransaction transaction) : base(transaction) { }
    }
}
