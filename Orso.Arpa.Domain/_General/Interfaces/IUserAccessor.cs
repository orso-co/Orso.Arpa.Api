using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.General.Interfaces
{
    public interface IUserAccessor : ITokenAccessor
    {
        Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default);
        Task<Person> GetCurrentPersonAsync(CancellationToken cancellationToken = default);
    }
}
