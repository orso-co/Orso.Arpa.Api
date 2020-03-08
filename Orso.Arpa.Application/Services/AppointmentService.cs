using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.AppointmentParticipations;
using Orso.Arpa.Application.Logic.Appointments;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Logic.Appointments;
using static Orso.Arpa.Application.Logic.Appointments.AddProject;

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

        public async Task AddProjectAsync(Dto addProjectDto)
        {
            Domain.Logic.Appointments.AddProject.Command command = _mapper.Map<Domain.Logic.Appointments.AddProject.Command>(addProjectDto);
            await _mediator.Send(command);
        }

        public async Task AddSectionAsync(Logic.Appointments.AddSection.Dto addSectionDto)
        {
            Domain.Logic.Appointments.AddSection.Command command = _mapper.Map<Domain.Logic.Appointments.AddSection.Command>(addSectionDto);
            await _mediator.Send(command);
        }

        public async Task AddRoomAsync(Logic.Appointments.AddRoom.Dto addRoomDto)
        {
            Domain.Logic.Appointments.AddRoom.Command command = _mapper.Map<Domain.Logic.Appointments.AddRoom.Command>(addRoomDto);
            await _mediator.Send(command);
        }

        public async Task<AppointmentDto> CreateAsync(Logic.Appointments.Create.Dto appointmentCreateDto)
        {
            Domain.Logic.Appointments.Create.Command command = _mapper.Map<Domain.Logic.Appointments.Create.Command>(appointmentCreateDto);
            Appointment createdAppointment = await _mediator.Send(command);
            return _mapper.Map<AppointmentDto>(createdAppointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range)
        {
            date ??= DateTime.Today;

            IQueryable<Appointment> appointments = await _mediator.Send(new List.Query<Appointment>(a =>
               a.StartTime >= DateHelper.GetStartTime(date.Value, range)
               && a.StartTime <= DateHelper.GetEndTime(date.Value, range)));

            return appointments
                .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
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
            Appointment appointment = await _mediator.Send(new Details.Query<Appointment>(id));
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
            AddParticipations(dto, appointment);
            return dto;
        }

        public async Task ModifyAsync(Logic.Appointments.Modify.Dto appointmentModifyDto)
        {
            Domain.Logic.Appointments.Modify.Command command = _mapper.Map<Domain.Logic.Appointments.Modify.Command>(appointmentModifyDto);
            await _mediator.Send(command);
        }

        public async Task RemoveProjectAsync(Logic.Appointments.RemoveProject.Dto removeProjectDto)
        {
            Domain.Logic.Appointments.RemoveProject.Command command = _mapper.Map<Domain.Logic.Appointments.RemoveProject.Command>(removeProjectDto);
            await _mediator.Send(command);
        }

        public async Task RemoveSectionAsync(Logic.Appointments.RemoveSection.Dto removeSectionDto)
        {
            Domain.Logic.Appointments.RemoveSection.Command command = _mapper.Map<Domain.Logic.Appointments.RemoveSection.Command>(removeSectionDto);
            await _mediator.Send(command);
        }

        public async Task RemoveRoomAsync(Logic.Appointments.RemoveRoom.Dto removeRoomDto)
        {
            Domain.Logic.Appointments.RemoveRoom.Command command = _mapper.Map<Domain.Logic.Appointments.RemoveRoom.Command>(removeRoomDto);
            await _mediator.Send(command);
        }

        public async Task SetDatesAsync(Logic.Appointments.SetDates.Dto setDatesDto)
        {
            Domain.Logic.Appointments.SetDates.Command command = _mapper.Map<Domain.Logic.Appointments.SetDates.Command>(setDatesDto);
            await _mediator.Send(command);
        }

        public async Task SetVenueAsync(Logic.Appointments.SetVenue.Dto setVenueDto)
        {
            Domain.Logic.Appointments.SetVenue.Command command = _mapper.Map<Domain.Logic.Appointments.SetVenue.Command>(setVenueDto);
            await _mediator.Send(command);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new Delete.Command<Appointment>() { Id = id });
        }

        public async Task SetParticipationResultAsync(Logic.AppointmentParticipations.SetResult.Dto setParticipationResult)
        {
            Domain.Logic.AppointmentParticipations.SetResult.Command command = _mapper.Map<Domain.Logic.AppointmentParticipations.SetResult.Command>(setParticipationResult);
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
