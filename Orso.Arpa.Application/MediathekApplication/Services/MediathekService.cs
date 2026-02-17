using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.MediathekApplication.Interfaces;
using Orso.Arpa.Application.MediathekApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MediathekDomain.Commands;
using Orso.Arpa.Domain.MediathekDomain.Enums;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Application.MediathekApplication.Services;

public class MediathekService : IMediathekService
{
    private readonly IArpaContext _arpaContext;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ITokenAccessor _tokenAccessor;

    public MediathekService(
        IArpaContext arpaContext,
        IMediator mediator,
        IMapper mapper,
        ITokenAccessor tokenAccessor)
    {
        _arpaContext = arpaContext;
        _mediator = mediator;
        _mapper = mapper;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<IEnumerable<MediathekAccessDto>> GetAllAccessesAsync()
    {
        List<MediathekAccess> accesses = await _arpaContext.MediathekAccesses
            .Include(a => a.Person)
                .ThenInclude(p => p.User)
            .OrderByDescending(a => a.GrantedAt)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MediathekAccessDto>>(accesses);
    }

    public async Task<MediathekAccessDto> GrantAccessAsync(GrantMediathekAccessDto dto)
    {
        var command = new GrantMediathekAccess.Command
        {
            PersonId = dto.PersonId,
            GrantedBy = _tokenAccessor.DisplayName,
            Notes = dto.Notes
        };

        MediathekAccess result = await _mediator.Send(command);
        return _mapper.Map<MediathekAccessDto>(result);
    }

    public async Task RevokeAccessAsync(Guid id)
    {
        await _mediator.Send(new RevokeMediathekAccess.Command { Id = id });
    }

    public async Task<IEnumerable<MediathekAccessRequestDto>> GetPendingRequestsAsync()
    {
        List<MediathekAccessRequest> requests = await _arpaContext.MediathekAccessRequests
            .Include(r => r.Person)
                .ThenInclude(p => p.User)
            .Include(r => r.Person)
                .ThenInclude(p => p.MusicianProfiles)
                    .ThenInclude(mp => mp.Instrument)
            .Where(r => r.Status == MediathekAccessRequestStatus.Pending)
            .OrderBy(r => r.RequestedAt)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MediathekAccessRequestDto>>(requests);
    }

    public async Task<MediathekAccessDto> ApproveRequestAsync(Guid id)
    {
        var command = new ApproveMediathekAccessRequest.Command
        {
            Id = id,
            ProcessedBy = _tokenAccessor.DisplayName
        };

        MediathekAccess result = await _mediator.Send(command);
        return _mapper.Map<MediathekAccessDto>(result);
    }

    public async Task DenyRequestAsync(Guid id)
    {
        await _mediator.Send(new DenyMediathekAccessRequest.Command
        {
            Id = id,
            ProcessedBy = _tokenAccessor.DisplayName
        });
    }

    public async Task<MediathekMyAccessDto> GetMyAccessAsync()
    {
        Guid personId = _tokenAccessor.PersonId;

        bool hasAccess = await _arpaContext.MediathekAccesses
            .AnyAsync(a => a.PersonId == personId && a.IsActive);

        bool hasPendingRequest = await _arpaContext.MediathekAccessRequests
            .AnyAsync(r => r.PersonId == personId && r.Status == MediathekAccessRequestStatus.Pending);

        return new MediathekMyAccessDto
        {
            HasAccess = hasAccess,
            HasPendingRequest = hasPendingRequest
        };
    }

    public async Task<MediathekAccessRequestDto> RequestAccessAsync(RequestMediathekAccessDto dto)
    {
        var command = new RequestMediathekAccess.Command
        {
            PersonId = _tokenAccessor.PersonId,
            Message = dto.Message
        };

        MediathekAccessRequest result = await _mediator.Send(command);
        return _mapper.Map<MediathekAccessRequestDto>(result);
    }

    public async Task<bool> CheckAccessByUsernameAsync(string username)
    {
        return await _arpaContext.MediathekAccesses
            .Include(a => a.Person)
                .ThenInclude(p => p.User)
            .AnyAsync(a => a.IsActive && a.Person.User != null && a.Person.User.NormalizedUserName == username.ToUpperInvariant());
    }
}
