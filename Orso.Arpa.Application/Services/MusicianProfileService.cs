using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

namespace Orso.Arpa.Application.Services
{
    public class MusicianProfileService : BaseReadOnlyService<
        MusicianProfileDto,
        MusicianProfile>, IMusicianProfileService
    {
        public MusicianProfileService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto createDto)
        {
            Domain.Logic.MusicianProfiles.Create.Command command = _mapper.Map<Domain.Logic.MusicianProfiles.Create.Command>(createDto);
            MusicianProfile createdEntity = await _mediator.Send(command);
            return _mapper.Map<MusicianProfileDto>(createdEntity);
        }

        public Task<IEnumerable<MusicianProfileDto>> GetByPersonAsync(Guid personId, bool includeDeactivated)
        {
            Expression<Func<MusicianProfile, bool>> predicate = includeDeactivated
                ? mp => mp.PersonId == personId
                : mp => mp.PersonId == personId && !mp.IsDeactivated;

            return GetAsync(predicate, orderBy: profile => profile.OrderByDescending(p => p.IsMainProfile));
        }

        public async Task<IEnumerable<ProjectParticipationDto>> GetProjectParticipationsAsync(Guid id, bool includeCompleted)
        {
            var query = new GetForMusicianProfile.Query { IncludeCompletedProjects = includeCompleted, MusicianProfileId = id };
            IEnumerable<ProjectParticipation> participations = await _mediator.Send(query);

            // Cannot use .ProjectTo here because .ProjectTo does not suppert After Mapping Actions
            return _mapper.Map<IEnumerable<ProjectParticipationDto>>(participations);
        }

        public Task SetActiveStatusAsync(Guid id, bool active)
        {
            var command = new SetActiveStatus.Command { Id = id, Active = active };
            return _mediator.Send(command);
        }
    }
}
