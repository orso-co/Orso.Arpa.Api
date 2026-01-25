using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Model;

namespace Orso.Arpa.Application.ProjectApplication.Interfaces
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
        Task<IEnumerable<AppointmentListDto>> GetAppointmentsByIdAsync(Guid id);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsWithParticipationsByIdAsync(Guid id);
        Task SetSetlistAsync(Guid projectId, Guid? setlistId);

        /// <summary>
        /// Resolves a list of project participation IDs to their project IDs.
        /// Used for activity feeds where audit logs may not contain the project ID.
        /// </summary>
        /// <param name="participationIds">List of project participation IDs to resolve</param>
        /// <returns>Dictionary mapping participation ID to project ID</returns>
        Task<Dictionary<Guid, Guid>> ResolveParticipationProjectIdsAsync(IEnumerable<Guid> participationIds);
    }
}
