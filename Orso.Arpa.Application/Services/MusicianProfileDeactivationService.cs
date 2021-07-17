using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfileDeactivations;

namespace Orso.Arpa.Application.Services
{
    public class MusicianProfileDeactivationService
        : BaseCreateService<MusicianProfileDeactivationDto, MusicianProfileDeactivation, MusicianProfileDeactivationCreateDto, Create.Command>, IMusicianProfileDeactivationService
    {
        public MusicianProfileDeactivationService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task DeleteByMusicianProfileAsync(Guid id)
        {
            var command = new Delete.Command { MusicianProfileId = id };
            await _mediator.Send(command);
        }
    }
}
