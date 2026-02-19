using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MediathekApplication.Model;

namespace Orso.Arpa.Application.MediathekApplication.Interfaces;

public interface IMediathekService
{
    Task<IEnumerable<MediathekAccessDto>> GetAllAccessesAsync();
    Task<MediathekAccessDto> GrantAccessAsync(GrantMediathekAccessDto dto);
    Task RevokeAccessAsync(Guid id);
    Task<IEnumerable<MediathekAccessRequestDto>> GetPendingRequestsAsync();
    Task<MediathekAccessDto> ApproveRequestAsync(Guid id);
    Task DenyRequestAsync(Guid id);
    Task<MediathekMyAccessDto> GetMyAccessAsync();
    Task<MediathekAccessRequestDto> RequestAccessAsync(RequestMediathekAccessDto dto);
    Task<bool> CheckAccessByUsernameAsync(string username);
}
