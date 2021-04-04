using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.UrlApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUrlService
    {
        Task AddAsync(Guid id, UrlCreateDto urlCreateDto);
        Task PutAsync(Guid id, Guid urlId, UrlModifyDto urlModifyDto);
        Task DeleteAsync(Guid id, Guid urlId);
        Task AddRoleAsync(Guid id, Guid urlId, UrlAddRoleDto urlAddRoleDt);
        Task RemoveRoleAsync(Guid id, Guid urlId, Guid roledId);
    }
}
