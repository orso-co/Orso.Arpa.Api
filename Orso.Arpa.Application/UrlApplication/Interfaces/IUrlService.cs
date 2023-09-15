using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.UrlApplication.Model;

namespace Orso.Arpa.Application.UrlApplication.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDto> GetByIdAsync(Guid id);
        Task<UrlDto> CreateAsync(UrlCreateDto createUrlDto);
        Task ModifyAsync(UrlModifyDto urlModifyDto);
        Task<UrlDto> AddRoleAsync(UrlAddRoleDto addRoleDto);
        Task RemoveRoleAsync(UrlRemoveRoleDto removeRoleDto);
        Task DeleteAsync(Guid id);
    }
}
