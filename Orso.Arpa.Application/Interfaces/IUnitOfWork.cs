using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
