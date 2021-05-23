using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMusicianProfileService
    {
        Task<IEnumerable<MusicianProfileDto>> GetAsync(Guid personId);
        Task<MusicianProfileDto> GetByIdAsync(Guid id);

        Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateBodyDto musicianProfileCreateDto);
        Task ModifyAsync(MusicianProfileModifyDto musicianProfileModifyDto);
        Task DeleteAsync(Guid id);
    }
}
