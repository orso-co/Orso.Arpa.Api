using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.ProjectApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAsync(bool includeCompleted);
        Task<ProjectDto> GetByIdAsync(Guid id);

        Task<ProjectDto> CreateAsync(ProjectCreateDto projectCreateDto);
        Task ModifyAsync(ProjectModifyDto projectModifyDto);
        Task DeleteAsync(Guid id);

        Task PostUrlAsync(Guid id, UrlDto urlDto);
        Task PutUrlAsync(Guid id, Guid urlId, UrlDto urlDto);
        Task DeleteUrlAsync(Guid id, Guid urlId);
    }
}
