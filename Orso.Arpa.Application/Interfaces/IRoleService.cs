using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.Roles;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAsync();
    }
}
