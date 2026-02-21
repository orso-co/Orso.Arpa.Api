using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AppointmentApplication.Interfaces;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Queries;
using Orso.Arpa.Domain.AppointmentDomain.Util;
using Orso.Arpa.Domain.AuditLogDomain.Enums;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Queries;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Queries;
using Orso.Arpa.Persistence.Migrations;

namespace Orso.Arpa.Application.AppointmentApplication.Services
{
    public class AppointmentService : BaseService<
        AppointmentDto,
        Appointment,
        AppointmentCreateDto,
        CreateAppointment.Command,
        AppointmentModifyDto,
        AppointmentModifyBodyDto,
        ModifyAppointment.Command>, IAppointmentService
    {
        private readonly IArpaContext _arpaContext;

        public AppointmentService(IMediator mediator, IMapper mapper, IArpaContext arpaContext) : base(mediator, mapper)
        {
            _arpaContext = arpaContext;
        }

        public async Task<AppointmentDto> AddProjectAsync(AppointmentAddProjectDto addProjectDto, bool includeParticipations)
        {
            AddProjectToAppointment.Command command = _mapper.Map<AddProjectToAppointment.Command>(addProjectDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addProjectDto.Id, includeParticipations);
        }

        public async Task<AppointmentDto> AddSectionAsync(AppointmentAddSectionDto addSectionDto, bool includeParticipations)
        {
            AddSectionToAppointment.Command command = _mapper.Map<AddSectionToAppointment.Command>(addSectionDto);
            await _mediator.Send(command);
            return await GetByIdAsync(addSectionDto.Id, includeParticipations);
        }

        public async Task AddRoomAsync(AppointmentAddRoomDto addRoomDto)
        {
            AddRoomToAppointment.Command command = _mapper.Map<AddRoomToAppointment.Command>(addRoomDto);
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

            return [.. _mapper.ProjectTo<AppointmentListDto>(entities)];
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAllAsync(DateTime? date, DateRange range)
        {
            IEnumerable<AppointmentListDto> result = await GetAsync(date, range);
            // Strip internal details for non-staff users
            foreach (AppointmentListDto dto in result)
            {
                dto.InternalDetails = null;
            }
            return result;
        }

        public async Task<IEnumerable<AppointmentRecentlyModifiedDto>> GetRecentlyModifiedAsync(int days)
        {
            DateTime cutoff = DateTime.UtcNow.AddDays(-days);

            IQueryable<Appointment> entities = await _mediator.Send(new List.Query<Appointment>(
                predicate: a => a.ModifiedAt != null && a.ModifiedAt >= cutoff,
                asSplitQuery: true));

            IEnumerable<AppointmentListDto> listDtos = [.. _mapper.ProjectTo<AppointmentListDto>(
                entities.OrderByDescending(a => a.ModifiedAt))];

            List<Guid> appointmentIds = listDtos.Select(d => d.Id).ToList();

            // Load latest audit log per appointment (TableName can be "Appointment" or "AppointmentProxy" due to EF lazy loading proxies)
            List<AuditLog> auditLogs = await _arpaContext.AuditLogs
                .Where(al => al.TableName.StartsWith("Appointment") && al.Type == AuditLogType.Update)
                .OrderByDescending(al => al.CreatedAt)
                .ToListAsync();

            Dictionary<Guid, AuditLog> latestAuditByAppointment = [];
            foreach (AuditLog log in auditLogs)
            {
                Guid? appointmentId = ExtractAppointmentId(log.KeyValues);
                if (appointmentId.HasValue && appointmentIds.Contains(appointmentId.Value) && !latestAuditByAppointment.ContainsKey(appointmentId.Value))
                {
                    latestAuditByAppointment[appointmentId.Value] = log;
                }
            }

            // Pre-load lookup tables for FK resolution
            Dictionary<Guid, string> venueNames = await _arpaContext.Venues
                .ToDictionaryAsync(v => v.Id, v => v.Name);
            Dictionary<Guid, string> selectValueNames = await _arpaContext.SelectValueMappings
                .Include(svm => svm.SelectValue)
                .Where(svm => svm.SelectValue != null)
                .ToDictionaryAsync(svm => svm.Id, svm => svm.SelectValue.Name);

            var result = new List<AppointmentRecentlyModifiedDto>();
            foreach (AppointmentListDto listDto in listDtos)
            {
                var dto = new AppointmentRecentlyModifiedDto
                {
                    Id = listDto.Id,
                    StartTime = listDto.StartTime,
                    EndTime = listDto.EndTime,
                    Name = listDto.Name,
                    City = listDto.City,
                    VenueName = listDto.VenueName,
                    Status = listDto.Status,
                    Type = listDto.Type,
                    Category = listDto.Category,
                    Projects = listDto.Projects,
                    PublicDetails = listDto.PublicDetails,
                    ModifiedAt = listDto.ModifiedAt,
                };

                if (latestAuditByAppointment.TryGetValue(listDto.Id, out AuditLog audit))
                {
                    dto.ModifiedBy = audit.CreatedBy;
                    dto.Changes = BuildChanges(audit, venueNames, selectValueNames);
                }

                result.Add(dto);
            }
            return result;
        }

        private static Guid? ExtractAppointmentId(string keyValues)
        {
            try
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, Guid>>(keyValues);
                return dict?.GetValueOrDefault("Id");
            }
            catch
            {
                return null;
            }
        }

        private static readonly HashSet<string> s_skipColumns =
        [
            "InternalDetails", "SalaryId", "SalaryPatternId", "ModifiedAt", "ModifiedBy",
            "CreatedAt", "CreatedBy", "DeletedAt", "AuditionId"
        ];

        private static readonly Dictionary<string, string> s_columnToFieldKey = new()
        {
            ["Name"] = "appointment-form.NAME",
            ["StartTime"] = "appointment-form.START_TIME",
            ["EndTime"] = "appointment-form.END_TIME",
            ["Status"] = "appointment-form.STATUS",
            ["Type"] = "appointment-form.TYPE",
            ["PublicDetails"] = "appointment-form.PUBLIC_DETAILS",
            ["CategoryId"] = "appointment-form.CATEGORY",
            ["VenueId"] = "appointment-form.VENUE",
            ["ExpectationId"] = "appointment-form.EXPECTATION",
        };

        private static IList<AppointmentChangeDto> BuildChanges(
            AuditLog audit,
            Dictionary<Guid, string> venueNames,
            Dictionary<Guid, string> selectValueNames)
        {
            var changes = new List<AppointmentChangeDto>();

            foreach (string column in audit.ChangedColumns)
            {
                if (s_skipColumns.Contains(column))
                    continue;

                if (!s_columnToFieldKey.TryGetValue(column, out string fieldKey))
                    continue;

                audit.OldValues.TryGetValue(column, out object oldRaw);
                audit.NewValues.TryGetValue(column, out object newRaw);

                string oldValue = ResolveValue(column, oldRaw, venueNames, selectValueNames);
                string newValue = ResolveValue(column, newRaw, venueNames, selectValueNames);

                if (oldValue != newValue)
                {
                    changes.Add(new AppointmentChangeDto
                    {
                        FieldKey = fieldKey,
                        OldValue = oldValue,
                        NewValue = newValue,
                    });
                }
            }
            return changes;
        }

        private static string ResolveValue(
            string column,
            object raw,
            Dictionary<Guid, string> venueNames,
            Dictionary<Guid, string> selectValueNames)
        {
            if (raw == null)
                return null;

            string str = raw.ToString();

            switch (column)
            {
                case "StartTime":
                case "EndTime":
                    if (DateTime.TryParse(str, out DateTime dt))
                        return dt.ToString("dd.MM.yyyy HH:mm");
                    return str;

                case "Status":
                    if (int.TryParse(str, out int statusInt) && Enum.IsDefined(typeof(AppointmentStatus), statusInt))
                        return $"appointment-status.{ToScreamingSnake(Enum.GetName(typeof(AppointmentStatus), statusInt))}";
                    if (Enum.TryParse<AppointmentStatus>(str, true, out var statusEnum))
                        return $"appointment-status.{ToScreamingSnake(statusEnum.ToString())}";
                    return str;

                case "Type":
                    if (int.TryParse(str, out int typeInt) && Enum.IsDefined(typeof(AppointmentType), typeInt))
                        return $"appointment-type.{ToScreamingSnake(Enum.GetName(typeof(AppointmentType), typeInt))}";
                    if (Enum.TryParse<AppointmentType>(str, true, out var typeEnum))
                        return $"appointment-type.{ToScreamingSnake(typeEnum.ToString())}";
                    return str;

                case "VenueId":
                    if (Guid.TryParse(str, out Guid venueId) && venueNames.TryGetValue(venueId, out string venueName))
                        return venueName;
                    return null;

                case "CategoryId":
                case "ExpectationId":
                    if (Guid.TryParse(str, out Guid svmId) && selectValueNames.TryGetValue(svmId, out string svmName))
                        return svmName;
                    return null;

                default:
                    return str;
            }
        }

        private static string ToScreamingSnake(string pascalCase)
        {
            return Regex.Replace(pascalCase, "(?<!^)([A-Z])", "_$1").ToUpperInvariant();
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

        private async Task AddParticipationsAsync(
            AppointmentDto dto,
            Appointment appointment,
            IEnumerable<ITree<Section>> flattenedTree)
        {
            var query = new ListParticipationsForAppointment.Query
            {
                Appointment = appointment,
                SectionTree = flattenedTree,
            };

            IEnumerable<ListParticipationsForAppointment.PersonGrouping> personGrouping = await _mediator.Send(query);

            dto.Participations = _mapper.Map<IList<AppointmentParticipationListItemDto>>(personGrouping);
        }

        public async Task<AppointmentDto> RemoveProjectAsync(AppointmentRemoveProjectDto removeProjectDto, bool includeParticipations)
        {
            RemoveProjectFromAppointment.Command command = _mapper.Map<RemoveProjectFromAppointment.Command>(removeProjectDto);
            await _mediator.Send(command);
            return await GetByIdAsync(removeProjectDto.Id, includeParticipations);
        }

        public async Task<AppointmentDto> RemoveSectionAsync(AppointmentRemoveSectionDto removeSectionDto, bool includeParticipations)
        {
            RemoveSectionFromAppointment.Command command = _mapper.Map<RemoveSectionFromAppointment.Command>(removeSectionDto);
            await _mediator.Send(command);
            return await GetByIdAsync(removeSectionDto.Id, includeParticipations);
        }

        public async Task RemoveRoomAsync(AppointmentRemoveRoomDto removeRoomDto)
        {
            RemoveRoomFromAppointment.Command command = _mapper.Map<RemoveRoomFromAppointment.Command>(removeRoomDto);
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

        public async Task SetParticipationResultAsync(AppointmentParticipationSetResultDto setParticipationResult)
        {
            SetAppointmentParticipationResult.Command command = _mapper.Map<SetAppointmentParticipationResult.Command>(setParticipationResult);
            await _mediator.Send(command);
        }

        public async Task SetParticipationPredictionAsync(AppointmentParticipationSetPredictionDto setParticipationPrediction)
        {
            SetAppointmentParticipationPrediction.Command command = _mapper.Map<SetAppointmentParticipationPrediction.Command>(setParticipationPrediction);
            await _mediator.Send(command);
        }

        public async Task SetParticipationCommentByStaffAsync(AppointmentParticipationSetCommentByStaffDto setCommentByStaff)
        {
            SetAppointmentParticipationCommentByStaff.Command command = _mapper.Map<SetAppointmentParticipationCommentByStaff.Command>(setCommentByStaff);
            await _mediator.Send(command);
        }

        public async Task SendAppointmentChangedNotificationAsync(SendAppointmentChangedNotificationDto sendAppointmentChangedNotificationDto)
        {
            SendAppointmentChangedNotification.Command command = _mapper.Map<SendAppointmentChangedNotification.Command>(sendAppointmentChangedNotificationDto);
            await _mediator.Send(command);
        }

        public async Task<string> ExportAppointmentsToIcsAsync()
        {
            var query = new ExportAppointmentsToIcs.Query();
            return await _mediator.Send(query);
        }

        public async Task<AppointmentDto> CopyAsync(AppointmentCopyDto appointmentCopyDto)
        {
            CopyAppointment.Command command = _mapper.Map<CopyAppointment.Command>(appointmentCopyDto);
            Appointment createdEntity = await _mediator.Send(command);
            return _mapper.Map<AppointmentDto>(createdEntity);
        }

        public async Task AddPrioritizedPieceAsync(Guid appointmentId, Guid setlistPieceId)
        {
            var command = new AddPrioritizedPiece.Command
            {
                AppointmentId = appointmentId,
                SetlistPieceId = setlistPieceId
            };
            await _mediator.Send(command);
        }

        public async Task RemovePrioritizedPieceAsync(Guid appointmentId, Guid setlistPieceId)
        {
            var command = new RemovePrioritizedPiece.Command
            {
                AppointmentId = appointmentId,
                SetlistPieceId = setlistPieceId
            };
            await _mediator.Send(command);
        }
    }
}
