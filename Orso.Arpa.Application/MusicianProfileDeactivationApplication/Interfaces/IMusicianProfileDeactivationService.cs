using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model;

namespace Orso.Arpa.Application.MusicianProfileDeactivationApplication.Interfaces
{
    public interface IMusicianProfileDeactivationService
    {
        Task<MusicianProfileDeactivationDto> CreateAsync(MusicianProfileDeactivationCreateDto musicianProfileDeactivationCreateDto);
        Task DeleteByMusicianProfileAsync(Guid id);
    }
}
