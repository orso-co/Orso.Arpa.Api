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
    }
}
