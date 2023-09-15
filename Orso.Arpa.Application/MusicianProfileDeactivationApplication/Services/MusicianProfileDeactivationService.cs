using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Interfaces;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.MusicianProfileDeactivationApplication.Services
{
    public class MusicianProfileDeactivationService
        : BaseCreateService<
            MusicianProfileDeactivationDto,
            MusicianProfileDeactivation,
            MusicianProfileDeactivationCreateDto,
            CreateMusicianProfileDeactivation.Command>, IMusicianProfileDeactivationService
    {
        public MusicianProfileDeactivationService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task DeleteByMusicianProfileAsync(Guid id)
        {
            var command = new DeleteMusicianProfileDeactivation.Command { MusicianProfileId = id };
            await _mediator.Send(command);
        }
    }
}
