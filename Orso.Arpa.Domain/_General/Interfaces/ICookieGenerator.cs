using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface ICookieGenerator 
    {
        Task<string> CreateTokensAsync(User user, string remoteIpAddress, CancellationToken cancellationToken);
    }
}
