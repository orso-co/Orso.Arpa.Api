using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface IJwtGenerator
    {
        Task CreateRefreshTokenAsync(User user, string remoteIpAddress, CancellationToken cancellationToken);

        Task<ClaimsIdentity> CreateClaimsIdentity(User user);
    }
}
