using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateTokensAsync(User user, string remoteIpAddress, CancellationToken cancellationToken);
    }
}
