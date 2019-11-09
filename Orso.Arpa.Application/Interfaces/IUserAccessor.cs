using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUserAccessor
    {
        string GetCurrentUserName();
        Task<User> GetCurrentUserAsync();
        IEnumerable<string> GetCurrentUserRoles();
    }
}
