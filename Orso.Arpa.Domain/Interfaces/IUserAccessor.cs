using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IUserAccessor : ITokenAccessor
    {
        Task<User> GetCurrentUserAsync();
    }
}
