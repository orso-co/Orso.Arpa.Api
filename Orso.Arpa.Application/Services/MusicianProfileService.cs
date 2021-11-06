using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.DoublingInstrumentApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Domain.Logic.ProjectParticipations;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Application.Services
{
    public class MusicianProfileService : BaseReadOnlyService<
        MusicianProfileDto,
        MusicianProfile>, IMusicianProfileService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public MusicianProfileService(IMediator mediator, IMapper mapper, IDateTimeProvider dateTimeProvider) : base(mediator, mapper)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto createDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(createDto);
            MusicianProfile createdEntity = await _mediator.Send(command);
            foreach (DoublingInstrumentCreateBodyDto doublingInstrument in createDto.Body.DoublingInstruments)
            {
                Orso.Arpa.Domain.Logic.MusicianProfileSections.Create.Command doublingInstrumentCommand = _mapper.Map<Domain.Logic.MusicianProfileSections.Create.Command>(doublingInstrument);
                doublingInstrumentCommand.MusicianProfileId = createdEntity.Id;
                await _mediator.Send(doublingInstrumentCommand);
            }
            return _mapper.Map<MusicianProfileDto>(createdEntity);
        }

        public Task<IEnumerable<MusicianProfileDto>> GetByPersonAsync(Guid personId, bool includeDeactivated)
        {
            Expression<Func<MusicianProfile, bool>> predicate = includeDeactivated
                ? mp => mp.PersonId == personId
                : mp => mp.PersonId == personId && (mp.Deactivation == null || mp.Deactivation.DeactivationStart > _dateTimeProvider.GetUtcNow());

            return GetAsync(predicate, orderBy: profile => profile.OrderByDescending(p => p.IsMainProfile));
        }

        public async Task<IEnumerable<GroupedMusicianProfileDto>> GetGroupedAsync()
        {
            var query = new GetGrouped.Query();
            IQueryable<Person> persons = await _mediator.Send(query);

            List<Person> list = await persons.ToListAsync();
            return _mapper.Map<IEnumerable<GroupedMusicianProfileDto>>(list);
        }

        public async Task<IEnumerable<ProjectParticipationDto>> GetProjectParticipationsAsync(Guid id, bool includeCompleted)
        {
            var query = new GetForMusicianProfile.Query { IncludeCompletedProjects = includeCompleted, MusicianProfileId = id };
            IEnumerable<ProjectParticipation> participations = await _mediator.Send(query);

            // Cannot use .ProjectTo here because .ProjectTo does not suppert After Mapping Actions
            return _mapper.Map<IEnumerable<ProjectParticipationDto>>(participations);
        }

        public async Task<MusicianProfileDto> UpdateAsync(MusicianProfileModifyDto musicianProfileModifyDto)
        {
            MusicianProfile existingMusicianProfile = await _mediator.Send(new Domain.GenericHandlers.Details.Query<MusicianProfile>(musicianProfileModifyDto.Id));

            Modify.Command command = _mapper.Map<Modify.Command>(musicianProfileModifyDto);

            command.InstrumentId = existingMusicianProfile.InstrumentId;
            command.ExistingMusicianProfile = existingMusicianProfile;

            await _mediator.Send(command);
            return await GetByIdAsync(command.Id);
        }
    }
}
