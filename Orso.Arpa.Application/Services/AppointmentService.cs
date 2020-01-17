using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Appointments;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AppointmentService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task AddProjectAsync(AddProjectDto addProjectDto)
        {
            AddProject.Command command = _mapper.Map<AddProject.Command>(addProjectDto);
            await _mediator.Send(command);
        }

        public async Task AddSectionAsync(AddSectionDto addSectionDto)
        {
            AddSection.Command command = _mapper.Map<AddSection.Command>(addSectionDto);
            await _mediator.Send(command);
        }

        public async Task AddRoomAsync(AddRoomDto addRoomDto)
        {
            AddRoom.Command command = _mapper.Map<AddRoom.Command>(addRoomDto);
            await _mediator.Send(command);
        }

        public async Task<AppointmentDto> CreateAsync(AppointmentCreateDto appointmentCreateDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(appointmentCreateDto);
            Appointment createdAppointment = await _mediator.Send(command);
            return _mapper.Map<AppointmentDto>(createdAppointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range)
        {
            date ??= DateTime.Today;

            IImmutableList<Appointment> appointments = await _mediator.Send(new List.Query
            {
                StartTime = DateHelper.GetStartTime(date.Value, range),
                EndTime = DateHelper.GetEndTime(date.Value, range)
            });
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
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

        public async Task<AppointmentDto> GetAsync(Guid id)
        {
            Appointment appointment = await _mediator.Send(new Details.Query { Id = id });
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
            AddParticipations(dto, appointment);
            return dto;
        }

        public async Task ModifyAsync(AppointmentModifyDto appointmentModifyDto)
        {
            Modify.Command command = _mapper.Map<Modify.Command>(appointmentModifyDto);
            await _mediator.Send(command);
        }

        public async Task RemoveProjectAsync(RemoveProjectDto removeProjectDto)
        {
            RemoveProject.Command command = _mapper.Map<RemoveProject.Command>(removeProjectDto);
            await _mediator.Send(command);
        }

        public async Task RemoveSectionAsync(RemoveSectionDto removeSectionDto)
        {
            RemoveSection.Command command = _mapper.Map<RemoveSection.Command>(removeSectionDto);
            await _mediator.Send(command);
        }

        public async Task RemoveRoomAsync(RemoveRoomDto removeRoomDto)
        {
            RemoveRoom.Command command = _mapper.Map<RemoveRoom.Command>(removeRoomDto);
            await _mediator.Send(command);
        }

        public async Task SetDatesAsync(SetDatesDto setDatesDto)
        {
            SetDates.Command command = _mapper.Map<SetDates.Command>(setDatesDto);
            await _mediator.Send(command);
        }

        public async Task SetVenueAsync(SetVenueDto setVenueDto)
        {
            SetVenue.Command command = _mapper.Map<SetVenue.Command>(setVenueDto);
            await _mediator.Send(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new Delete.Command() { Id = id });
        }
    }

    internal class PersonGrouping
    {
        public Person Person { get; set; }
        public IEnumerable<MusicianProfile> Profiles { get; set; }
        public AppointmentParticipation Participation { get; set; }
    }
}
