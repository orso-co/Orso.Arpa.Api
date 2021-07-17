using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IMusicianProfileDeactivationService
    {
        Task<MusicianProfileDeactivationDto> CreateAsync(MusicianProfileDeactivationCreateDto musicianProfileDeactivationCreateDto);
        Task DeleteByMusicianProfileAsync(Guid id);
    }
}
