using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sword.Core
{
    public interface ITransaction : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
