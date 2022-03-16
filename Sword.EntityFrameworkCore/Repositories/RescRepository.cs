using Sword.Core;
using Sword.Domain;

namespace Sword.EntityFrameworkCore.Repositories
{
    public class RescRepository : SwordRepository<Resc>, IRescRepository
    {
        public RescRepository(ITransaction transaction) : base(transaction) { }
    }
}
