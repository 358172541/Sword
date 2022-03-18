using Core;
using Domain;

namespace EntityFrameworkCore.Repositories
{
    public class RescRepository : BaseRepository<Resc>, IRescRepository
    {
        public RescRepository(ITransaction transaction) : base(transaction) { }
    }
}
