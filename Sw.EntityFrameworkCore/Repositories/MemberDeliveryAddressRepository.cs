using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class MemberDeliveryAddressRepository : BaseRepository<MemberDeliveryAddress>, IMemberDeliveryAddressRepository
    {
        public MemberDeliveryAddressRepository(ITransaction transaction) : base(transaction) { }
    }
}
