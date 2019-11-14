using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
