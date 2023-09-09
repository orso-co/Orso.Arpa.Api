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
using Orso.Arpa.Domain.Extensions;
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
        AppointmentModifyBodyDto,
        Domain.Logic.Appointments.Modify.Command>, IAppointmentService
    {
        public AppointmentService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<AppointmentDto> AddProjectAsync(AppointmentAddProjectDto addProjectDto, bool includeParticipations)
        {
            AddProject.Command command = _mapper.Map<AddProject.Command>(addProjectDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addProjectDto.Id, includeParticipations);
        }

        public async Task<AppointmentDto> AddSectionAsync(AppointmentAddSectionDto addSectionDto, bool includeParticipations)
        {
            AddSection.Command command = _mapper.Map<AddSection.Command>(addSectionDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addSectionDto.Id, includeParticipations);
        }

        public async Task AddRoomAsync(AppointmentAddRoomDto addRoomDto)
        {
            AddRoom.Command command = _mapper.Map<AddRoom.Command>(addRoomDto);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAsync(DateTime? date, DateRange range)
        {
            date ??= DateTime.Today;

            DateTime rangeStartTime = DateHelper.GetStartTime(date.Value, range);
            DateTime rangeEndTime = DateHelper.GetEndTime(date.Value, range);

            IQueryable<Appointment> entities = await _mediator.Send(new List.Query<Appointment>(
                predicate: a =>
                    (a.EndTime <= rangeEndTime && a.EndTime >= rangeStartTime)
                    || (a.EndTime > rangeEndTime && a.StartTime <= rangeEndTime),
                asSplitQuery: true));

            return _mapper.ProjectTo<AppointmentListDto>(entities).ToList();
        }

        public async Task<AppointmentDto> GetByIdAsync(Guid id, bool includeParticipations)
        {
            Appointment appointment = await _mediator.Send(new Details.Query<Appointment>(id));
            AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
            if (includeParticipations)
            {
                var treeQuery = new Domain.Logic.Sections.FlattenedTree.Query();
                IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
                await AddParticipationsAsync(dto, appointment, flattenedTree);
            }
            return dto;
        }

        private async Task AddParticipationsAsync(
            AppointmentDto dto,
            Appointment appointment,
            IEnumerable<ITree<Section>> flattenedTree)
        {
            var query = new Domain.Logic.MusicianProfiles.GetForAppointment.Query
            {
                Appointment = appointment,
                SectionTree = flattenedTree,
            };

            IEnumerable<Domain.Logic.MusicianProfiles.GetForAppointment.PersonGrouping> personGrouping = await _mediator.Send(query);

            dto.Participations = _mapper.Map<IList<AppointmentParticipationListItemDto>>(personGrouping);
        }

        public async Task<AppointmentDto> RemoveProjectAsync(AppointmentRemoveProjectDto removeProjectDto, bool includeParticipations)
        {
            RemoveProject.Command command = _mapper.Map<RemoveProject.Command>(removeProjectDto);
            await _mediator.Send(command);
            return await GetByIdAsync(removeProjectDto.Id, includeParticipations);
        }

        public async Task<AppointmentDto> RemoveSectionAsync(AppointmentRemoveSectionDto removeSectionDto, bool includeParticipations)
        {
            RemoveSection.Command command = _mapper.Map<RemoveSection.Command>(removeSectionDto);
            await _mediator.Send(command);
            return await GetByIdAsync(removeSectionDto.Id, includeParticipations);
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
            var treeQuery = new Domain.Logic.Sections.FlattenedTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
            await AddParticipationsAsync(dto, appointment, flattenedTree);
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

        public async Task SetParticipationPredictionAsync(AppointmentParticipationSetPredictionDto setParticipationPrediction)
        {
            AppointmentParticipations.SetPrediction.Command command = _mapper.Map<AppointmentParticipations.SetPrediction.Command>(setParticipationPrediction);
            await _mediator.Send(command);
        }
    }
}
