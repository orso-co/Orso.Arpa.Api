using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Model;

namespace Orso.Arpa.Application.MusicianProfileApplication.Interfaces
{
    public interface IMusicianProfileService
    {
        Task<MusicianProfileDto> GetByIdAsync(Guid id);
        Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto musicianProfileCreateDto);
        Task<IEnumerable<MusicianProfileDto>> GetByPersonAsync(Guid personId, bool includeDeactivated);
        Task<IEnumerable<ProjectParticipationDto>> GetProjectParticipationsAsync(Guid id, bool includeCompleted);
        Task<MusicianProfileDto> UpdateAsync(MusicianProfileModifyDto musicianProfileModifyDto);
        Task<IEnumerable<GroupedMusicianProfileDto>> GetGroupedAsync();

        Task DeleteAsync(Guid id);

        Task<IEnumerable<MusicianProfileAppointmentParticipationDto>> GetAppointmentParticipationsAsync(Guid id, Guid? projectId, DateTime? startTime, DateTime? endTime);
    }
}
