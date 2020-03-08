using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Appointments;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Application.Services
{
    public class AppointmentService : BaseService<
        AppointmentDto,
        Appointment,
        AppointmentCreateDto,
        Create.Command,
        AppointmentModifyDto,
        Modify.Command>, IAppointmentService
    {
        public AppointmentService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task AddProjectAsync(AppointmentAddProjectDto addProjectDto)
        {
            AddProject.Command command = _mapper.Map<AddProject.Command>(addProjectDto);
            await _mediator.Send(command);
        }

        public async Task AddSectionAsync(AppointmentAddSectionDto addSectionDto)
        {
            AddSection.Command command = _mapper.Map<AddSection.Command>(addSectionDto);
            await _mediator.Send(command);
        }

        public async Task AddRoomAsync(AppointmentAddRoomDto addRoomDto)
        {
            AddRoom.Command command = _mapper.Map<AddRoom.Command>(addRoomDto);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range)
        {
            date ??= DateTime.Today;

            return await base.GetAsync(predicate: a =>
               a.StartTime >= DateHelper.GetStartTime(date.Value, range)
               && a.StartTime <= DateHelper.GetEndTime(date.Value, range));
        }

        private void AddParticipations(AppointmentDto dto, Appointment appointment)
        {
            IEnumerable<ProjectParticipation> participations = appointment.ProjectAppointments
                .Select(pa => pa.Project)
                .SelectMany(p => p.ProjectParticipations);

            IEnumerable<PersonGrouping> persons = from p in participations
                                                  group p by p.MusicianProfile.Person into g
                                                  select new PersonGrouping
                                                  {
                                                      Person = g.Key,
                                                      Profiles = g.ToList().Select(g => g.MusicianProfile),
                                                      Participation = appointment.AppointmentParticipations.FirstOrDefault(ap => ap.PersonId == g.Key.Id)
                                                  };

            dto.Participations = _mapper.Map<IList<AppointmentParticipationListItemDto>>(persons);
        }

        public async Task RemoveProjectAsync(AppointmentRemoveProjectDto removeProjectDto)
        {
            RemoveProject.Command command = _mapper.Map<RemoveProject.Command>(removeProjectDto);
            await _mediator.Send(command);
        }

        public async Task RemoveSectionAsync(AppointmentRemoveSectionDto removeSectionDto)
        {
            RemoveSection.Command command = _mapper.Map<RemoveSection.Command>(removeSectionDto);
            await _mediator.Send(command);
        }

        public async Task RemoveRoomAsync(AppointmentRemoveRoomDto removeRoomDto)
        {
            RemoveRoom.Command command = _mapper.Map<RemoveRoom.Command>(removeRoomDto);
            await _mediator.Send(command);
        }

        public async Task SetDatesAsync(AppointmentSetDatesDto setDatesDto)
        {
            SetDates.Command command = _mapper.Map<SetDates.Command>(setDatesDto);
            await _mediator.Send(command);
        }

        public async Task SetVenueAsync(AppointmentSetVenueDto setVenueDto)
        {
            SetVenue.Command command = _mapper.Map<SetVenue.Command>(setVenueDto);
            await _mediator.Send(command);
        }

        public async Task SetParticipationResultAsync(AppointmentParticipationSetResultDto setParticipationResult)
        {
            AppointmentParticipations.SetResult.Command command = _mapper.Map<AppointmentParticipations.SetResult.Command>(setParticipationResult);
            await _mediator.Send(command);
        }
    }

    internal class PersonGrouping
    {
        public Person Person { get; set; }
        public IEnumerable<MusicianProfile> Profiles { get; set; }
        public AppointmentParticipation Participation { get; set; }
    }
}
