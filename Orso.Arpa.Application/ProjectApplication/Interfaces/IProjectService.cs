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
        Task<IEnumerable<ProjectParticipationDto>> GetParticipationsByIdAsync(Guid id);
        Task<ProjectParticipationDto> SetProjectParticipationAsync(SetProjectParticipationDto myProjectParticipationDto);
    }
}
