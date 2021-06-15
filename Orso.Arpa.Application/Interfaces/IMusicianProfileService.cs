using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMusicianProfileService
    {
        Task<MusicianProfileDto> GetByIdAsync(Guid id);
        Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto musicianProfileCreateDto);
        Task<IEnumerable<MusicianProfileDto>> GetByPersonAsync(Guid personId, bool includeDeactivated);

        Task<IEnumerable<ProjectParticipationDto>> GetProjectParticipationsAsync(Guid id, bool includeCompleted);
        Task SetActiveStatusAsync(Guid id, bool active);
        Task<MusicianProfileDto> UpdateAsync(MusicianProfileModifyDto musicianProfileModifyDto);
    }
}
