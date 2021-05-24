using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMusicianProfileService
    {
        Task<MusicianProfileDto> GetByIdAsync(Guid id);
        Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto musicianProfileCreateDto);
        Task<IEnumerable<MusicianProfileDto>> GetByPersonAsync(Guid personId, bool includeDeactivated);
    }
}
