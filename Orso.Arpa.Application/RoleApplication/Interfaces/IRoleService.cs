using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.RoleApplication.Model;

namespace Orso.Arpa.Application.RoleApplication.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAsync();
    }
}
