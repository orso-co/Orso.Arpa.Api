using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Application.UrlApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAsync(bool includeCompleted);
        Task<ProjectDto> GetByIdAsync(Guid id);

        Task<ProjectDto> CreateAsync(ProjectCreateDto projectCreateDto);
        Task<UrlDto> AddUrlAsync(UrlCreateDto urlCreateDto);
        Task ModifyAsync(ProjectModifyDto projectModifyDto);
        Task DeleteAsync(Guid id);
    }
}
