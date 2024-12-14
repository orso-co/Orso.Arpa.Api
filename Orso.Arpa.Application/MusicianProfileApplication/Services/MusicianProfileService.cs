using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.MusicianProfileApplication.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Queries;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Notifications;
using Orso.Arpa.Domain.MusicianProfileDomain.Queries;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Queries;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Application.MusicianProfileApplication.Services
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

        public async Task<MusicianProfileDto> CreateAsync(MusicianProfileCreateDto musicianProfileCreateDto)
        {
            CreateMusicianProfile.Command command = _mapper.Map<CreateMusicianProfile.Command>(musicianProfileCreateDto);
            MusicianProfile createdEntity = await _mediator.Send(command);
            var notification = new MusicianProfileCreatedNotification { MusicianProfile = createdEntity };
            await _mediator.Publish(notification);

            return _mapper.Map<MusicianProfileDto>(createdEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            _ = await _mediator.Send(new Delete.Command<MusicianProfile>() { Id = id });
        }

        public async Task<IEnumerable<MusicianProfileAppointmentParticipationDto>> GetAppointmentParticipationsAsync(Guid id, Guid? projectId, DateTime? startTime, DateTime? endTime)
        {
            var query = new ListAppointmentParticipationsForMusicianProfile.Query
            {
                MusicianProfileId = id,
                ProjectId = projectId,
                StartTime = startTime,
                EndTime = endTime
            };
            IEnumerable<AppointmentParticipation> appointmentParticipations = await _mediator.Send(query);
            return _mapper.Map<IEnumerable<MusicianProfileAppointmentParticipationDto>>(appointmentParticipations);
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
            var query = new ListPersonsWithAtLeastOneMusicianProfile.Query();
            IQueryable<Person> persons = await _mediator.Send(query);

            List<Person> list = await persons.ToListAsync();
            return _mapper.Map<IEnumerable<GroupedMusicianProfileDto>>(list);
        }

        public async Task<IEnumerable<ProjectParticipationDto>> GetProjectParticipationsAsync(Guid id, bool includeCompleted)
        {
            var query = new ListProjectParticipationsForMusicianProfile.Query { IncludeCompletedProjects = includeCompleted, MusicianProfileId = id };
            IEnumerable<MusicianProfileProjectParticipationGrouping> participations = await _mediator.Send(query);

            // Cannot use .ProjectTo here because .ProjectTo does not support After Mapping Actions
            return _mapper.Map<IEnumerable<ProjectParticipationDto>>(participations);
        }

        public async Task<MusicianProfileDto> UpdateAsync(MusicianProfileModifyDto musicianProfileModifyDto)
        {
            MusicianProfile existingMusicianProfile = await _mediator.Send(new Details.Query<MusicianProfile>(musicianProfileModifyDto.Id));

            ModifyMusicianProfile.Command command = _mapper.Map<ModifyMusicianProfile.Command>(musicianProfileModifyDto);

            command.InstrumentId = existingMusicianProfile.InstrumentId;
            command.ExistingMusicianProfile = existingMusicianProfile;

            _ = await _mediator.Send(command);
            return await GetByIdAsync(command.Id);
        }
    }
}
