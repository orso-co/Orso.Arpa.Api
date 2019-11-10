using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUserAccessor
    {
        string UserName { get; }
        string DisplayName { get; }
        Task<User> GetCurrentUserAsync();
        IEnumerable<string> UserRoles { get; }
    }
}
