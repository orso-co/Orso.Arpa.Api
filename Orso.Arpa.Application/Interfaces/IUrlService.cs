using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.UrlApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDto> GetByIdAsync(Guid id);
        Task PutAsync(UrlModifyDto urlModifyDto);
        Task<UrlDto> AddRoleAsync(UrlAddRoleDto addRoleDto);
        Task RemoveRoleAsync(UrlRemoveRoleDto removeRoleDto);
        Task DeleteAsync(Guid id);
    }
}
