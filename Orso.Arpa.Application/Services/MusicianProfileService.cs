using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.Services
{
    public class MusicianProfileService : BaseService<
        MusicianProfileDto,
        MusicianProfile,
        MusicianProfileCreateBodyDto,
        Create.Command,
        MusicianProfileModifyDto,
        MusicianProfileModifyBodyDto,
        Modify.Command>, IMusicianProfileService
    {
        public MusicianProfileService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public Task<IEnumerable<MusicianProfileDto>> GetAsync(Guid personId)
        {
            return GetAsync(profile => profile.PersonId == personId);
        }
    }
}
