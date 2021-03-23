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
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Logic.Appointments;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Application.Services
{
    public class AppointmentService : BaseService<
        AppointmentDto,
        Appointment,
        AppointmentCreateDto,
        Domain.Logic.Appointments.Create.Command,
        AppointmentModifyDto,
        Domain.Logic.Appointments.Modify.Command>, IAppointmentService
    {
        public AppointmentService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<AppointmentDto> AddProjectAsync(AppointmentAddProjectDto addProjectDto)
        {
            AddProject.Command command = _mapper.Map<AddProject.Command>(addProjectDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addProjectDto.Id);
        }

        public async Task<AppointmentDto> AddSectionAsync(AppointmentAddSectionDto addSectionDto)
        {
            AddSection.Command command = _mapper.Map<AddSection.Command>(addSectionDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addSectionDto.Id);
        }

        public async Task AddRoomAsync(AppointmentAddRoomDto addRoomDto)
        {
            AddRoom.Command command = _mapper.Map<AddRoom.Command>(addRoomDto);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range)
        {
            date ??= DateTime.Today;

            IQueryable<Appointment> entities = await _mediator.Send(new List.Query<Appointment>(
                predicate: a =>
                    a.StartTime >= DateHelper.GetStartTime(date.Value, range)
                    && a.StartTime <= DateHelper.GetEndTime(date.Value, range)));

            var dtoList = new List<AppointmentDto>();
            foreach (Appointment appointment in entities)
            {
                AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
                AddParticipations(dto, appointment);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public override async Task<AppointmentDto> GetByIdAsync(Guid id)
        {
            Appointment appointment = await _mediator.Send(new Details.Query<Appointment>(id));
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
            AddParticipations(dto, appointment);
            return dto;
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

        public async Task<AppointmentDto> RemoveProjectAsync(AppointmentRemoveProjectDto removeProjectDto)
        {
            RemoveProject.Command command = _mapper.Map<RemoveProject.Command>(removeProjectDto);
            await _mediator.Send(command);
            return await GetByIdAsync(removeProjectDto.Id);
        }

        public async Task<AppointmentDto> RemoveSectionAsync(AppointmentRemoveSectionDto removeSectionDto)
        {
            RemoveSection.Command command = _mapper.Map<RemoveSection.Command>(removeSectionDto);
            await _mediator.Send(command);
            return await GetByIdAsync(removeSectionDto.Id);
        }

        public async Task RemoveRoomAsync(AppointmentRemoveRoomDto removeRoomDto)
        {
            RemoveRoom.Command command = _mapper.Map<RemoveRoom.Command>(removeRoomDto);
            await _mediator.Send(command);
        }

        public async Task<AppointmentDto> SetDatesAsync(AppointmentSetDatesDto setDatesDto)
        {
            SetDates.Command command = _mapper.Map<SetDates.Command>(setDatesDto);
            Appointment appointment = await _mediator.Send(command);
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
            AddParticipations(dto, appointment);
            return dto;
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
