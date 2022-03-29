using Core;
using Domain;
using Domain.IRepositories;

namespace EntityFrameworkCore.Repositories
{
    public class ContainerRepository : BaseRepository<Container>, IContainerRepository
    {
        public ContainerRepository(ITransaction transaction) : base(transaction) { }
    }
}
