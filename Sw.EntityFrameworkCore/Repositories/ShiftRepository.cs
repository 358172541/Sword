using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(ITransaction transaction) : base(transaction) { }
    }
}
