using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AppointmentApplication.Interfaces;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Util;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.MusicianProfileDomain.Queries;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Queries;

namespace Orso.Arpa.Application.AppointmentApplication.Services;

public class AppointmentService : BaseService<
    AppointmentDto,
    Appointment,
    AppointmentCreateDto,
    CreateAppointment.Command,
    AppointmentModifyDto,
    AppointmentModifyBodyDto,
    ModifyAppointment.Command>, IAppointmentService
{
    public AppointmentService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    public async Task<AppointmentDto> AddProjectAsync(AppointmentAddProjectDto addProjectDto,
        bool includeParticipations)
    {
        AddProjectToAppointment.Command command =
            _mapper.Map<AddProjectToAppointment.Command>(addProjectDto);
        await _mediator.Send(command);
        return await GetByIdAsync(addProjectDto.Id, includeParticipations);
    }

    public async Task<AppointmentDto> AddSectionAsync(AppointmentAddSectionDto addSectionDto,
        bool includeParticipations)
    {
        AddSectionToAppointment.Command command =
            _mapper.Map<AddSectionToAppointment.Command>(addSectionDto);
        await _mediator.Send(command);
        return await GetByIdAsync(addSectionDto.Id, includeParticipations);
    }

    public async Task AddRoomAsync(AppointmentAddRoomDto addRoomDto)
    {
        AddRoomToAppointment.Command
            command = _mapper.Map<AddRoomToAppointment.Command>(addRoomDto);
        await _mediator.Send(command);
    }

    public async Task<IEnumerable<AppointmentListDto>> GetAsync(DateTime? date, DateRange? range)
    {
        if (range.HasValue)
        {
            date ??= DateTime.Today;
            DateTime rangeStartTime = DateHelper.GetStartTime(date.Value, range.Value);
            DateTime rangeEndTime = DateHelper.GetEndTime(date.Value, range.Value);

            IQueryable<Appointment> entities = await _mediator.Send(new List.Query<Appointment>(
                a => (a.EndTime <= rangeEndTime && a.EndTime >= rangeStartTime) ||
                     (a.EndTime > rangeEndTime && a.StartTime <= rangeEndTime),
                asSplitQuery: true));

            return _mapper.ProjectTo<AppointmentListDto>(entities);
        }
        else
        {
            IQueryable<Appointment> entities = await _mediator.Send(new List.Query<Appointment>(
                asSplitQuery: true));

            return _mapper.ProjectTo<AppointmentListDto>(entities);
        }
    }

    public async Task<AppointmentDto> GetByIdAsync(Guid id, bool includeParticipations)
    {
        Appointment appointment = await _mediator.Send(new Details.Query<Appointment>(id));
        AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
        if (includeParticipations)
        {
            var treeQuery = new ListFlattenedSectionTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
            await AddParticipationsAsync(dto, appointment, flattenedTree);
        }

        return dto;
    }

    public async Task<AppointmentDto> RemoveProjectAsync(
        AppointmentRemoveProjectDto removeProjectDto, bool includeParticipations)
    {
        RemoveProjectFromAppointment.Command command =
            _mapper.Map<RemoveProjectFromAppointment.Command>(removeProjectDto);
        await _mediator.Send(command);
        return await GetByIdAsync(removeProjectDto.Id, includeParticipations);
    }

    public async Task<AppointmentDto> RemoveSectionAsync(
        AppointmentRemoveSectionDto removeSectionDto, bool includeParticipations)
    {
        RemoveSectionFromAppointment.Command command =
            _mapper.Map<RemoveSectionFromAppointment.Command>(removeSectionDto);
        await _mediator.Send(command);
        return await GetByIdAsync(removeSectionDto.Id, includeParticipations);
    }

    public async Task RemoveRoomAsync(AppointmentRemoveRoomDto removeRoomDto)
    {
        RemoveRoomFromAppointment.Command command =
            _mapper.Map<RemoveRoomFromAppointment.Command>(removeRoomDto);
        await _mediator.Send(command);
    }

    public async Task<AppointmentDto> SetDatesAsync(AppointmentSetDatesDto setDatesDto)
    {
        SetDates.Command command = _mapper.Map<SetDates.Command>(setDatesDto);
        Appointment appointment = await _mediator.Send(command);
        AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
        var treeQuery = new ListFlattenedSectionTree.Query();
        IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);
        await AddParticipationsAsync(dto, appointment, flattenedTree);
        return dto;
    }

    public async Task SetVenueAsync(AppointmentSetVenueDto setVenueDto)
    {
        SetVenue.Command command = _mapper.Map<SetVenue.Command>(setVenueDto);
        await _mediator.Send(command);
    }

    public async Task SetParticipationResultAsync(
        AppointmentParticipationSetResultDto setParticipationResult)
    {
        SetAppointmentParticipationResult.Command command =
            _mapper.Map<SetAppointmentParticipationResult.Command>(setParticipationResult);
        await _mediator.Send(command);
    }

    public async Task SetParticipationPredictionAsync(
        AppointmentParticipationSetPredictionDto setParticipationPrediction)
    {
        SetAppointmentParticipationPrediction.Command command =
            _mapper.Map<SetAppointmentParticipationPrediction.Command>(setParticipationPrediction);
        await _mediator.Send(command);
    }

    public async Task SendAppointmentChangedNotificationAsync(
        SendAppointmentChangedNotificationDto sendAppointmentChangedNotificationDto)
    {
        SendAppointmentChangedNotification.Command command =
            _mapper.Map<SendAppointmentChangedNotification.Command>(
                sendAppointmentChangedNotificationDto);
        await _mediator.Send(command);
    }

    private async Task AddParticipationsAsync(
        AppointmentDto dto,
        Appointment appointment,
        IEnumerable<ITree<Section>> flattenedTree)
    {
        var query = new ListParticipationsForAppointment.Query
        {
            Appointment = appointment, SectionTree = flattenedTree
        };

        IEnumerable<ListParticipationsForAppointment.PersonGrouping> personGrouping =
            await _mediator.Send(query);

        dto.Participations =
            _mapper.Map<IList<AppointmentParticipationListItemDto>>(personGrouping);
    }

    public async Task<string> ExportAppointmentsToIcsAsync()
    {
        var command = new ExportAppointmentsToIcs.Command();
        return await _mediator.Send(command);
    }
}
