using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AuditLogApplication.Interfaces;
using Orso.Arpa.Application.AuditLogApplication.Model;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.General.GenericHandlers;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Application.AuditLogApplication.Services
{
    public class AuditLogService : IAuditLogService
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;
        private readonly IArpaContext _arpaContext;

        public AuditLogService(IMediator mediator, IMapper mapper, IArpaContext arpaContext)
        {
            _mapper = mapper;
            _mediator = mediator;
            _arpaContext = arpaContext;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAsync(Guid? entityId, int? skip, int? take)
        {
            IQueryable<AuditLog> entities = await _mediator.Send(new List.Query<AuditLog>(
                predicate: entityId == null ? null : v => v.KeyValues.Contains(entityId.ToString()),
                orderBy: v => v.OrderByDescending(v => v.CreatedAt),
                skip: skip ?? 0,
                take: take ?? 25));

            return _mapper.Map<IEnumerable<AuditLogDto>>(entities);
        }

        public async Task<IEnumerable<RecentParticipationChangeDto>> GetRecentParticipationChangesAsync(int take = 20)
        {
            // Get recent audit logs for ProjectParticipation and AppointmentParticipation
            var auditLogs = await _arpaContext.AuditLogs
                .AsNoTracking()
                .Where(a => a.TableName == "ProjectParticipation" || a.TableName == "AppointmentParticipation")
                .OrderByDescending(a => a.CreatedAt)
                .Take(take * 2) // Take more to account for filtering
                .ToListAsync();

            var result = new List<RecentParticipationChangeDto>();

            foreach (var log in auditLogs)
            {
                var dto = await MapAuditLogToRecentChangeAsync(log);
                if (dto != null)
                {
                    result.Add(dto);
                }

                if (result.Count >= take)
                {
                    break;
                }
            }

            return result;
        }

        private async Task<RecentParticipationChangeDto> MapAuditLogToRecentChangeAsync(AuditLog log)
        {
            var dto = new RecentParticipationChangeDto
            {
                Id = log.Id,
                CreatedAt = log.CreatedAt,
                ParticipationType = log.TableName == "ProjectParticipation" ? "Project" : "Appointment"
            };

            // Get values from NewValues (for creates/updates) or OldValues (for deletes)
            var values = log.NewValues.Count > 0 ? log.NewValues : log.OldValues;

            // Extract status
            if (values.TryGetValue("ParticipationStatusInner", out var statusInner))
            {
                dto.StatusInner = statusInner?.ToString();
            }

            // Determine status result based on inner status
            dto.StatusResult = DetermineStatusResult(dto.StatusInner);

            if (log.TableName == "ProjectParticipation")
            {
                return await MapProjectParticipationAsync(dto, values);
            }
            else
            {
                return await MapAppointmentParticipationAsync(dto, values);
            }
        }

        private async Task<RecentParticipationChangeDto> MapProjectParticipationAsync(
            RecentParticipationChangeDto dto,
            Dictionary<string, object> values)
        {
            // Get MusicianProfileId to find the person
            if (values.TryGetValue("MusicianProfileId", out var mpIdObj) && TryParseGuid(mpIdObj, out var musicianProfileId))
            {
                var profile = await _arpaContext.MusicianProfiles
                    .AsNoTracking()
                    .Include(mp => mp.Person)
                    .FirstOrDefaultAsync(mp => mp.Id == musicianProfileId);

                if (profile?.Person != null)
                {
                    dto.PersonName = $"{profile.Person.GivenName} {profile.Person.Surname}";
                    dto.PersonId = profile.Person.Id;
                }
            }

            // Get ProjectId to find the project name
            if (values.TryGetValue("ProjectId", out var projIdObj) && TryParseGuid(projIdObj, out var projectId))
            {
                var project = await _arpaContext.Projects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == projectId);

                if (project != null)
                {
                    dto.TargetName = project.Title;
                    dto.TargetId = project.Id;
                }
                else
                {
                    dto.TargetName = "Projekt";
                    dto.TargetId = projectId;
                }
            }
            else
            {
                dto.TargetName = "Projekt";
            }

            return dto;
        }

        private async Task<RecentParticipationChangeDto> MapAppointmentParticipationAsync(
            RecentParticipationChangeDto dto,
            Dictionary<string, object> values)
        {
            // Get MusicianProfileId to find the person
            if (values.TryGetValue("MusicianProfileId", out var mpIdObj) && TryParseGuid(mpIdObj, out var musicianProfileId))
            {
                var profile = await _arpaContext.MusicianProfiles
                    .AsNoTracking()
                    .Include(mp => mp.Person)
                    .FirstOrDefaultAsync(mp => mp.Id == musicianProfileId);

                if (profile?.Person != null)
                {
                    dto.PersonName = $"{profile.Person.GivenName} {profile.Person.Surname}";
                    dto.PersonId = profile.Person.Id;
                }
            }

            // For appointment participations, extract Prediction status
            if (values.TryGetValue("Prediction", out var prediction))
            {
                dto.StatusInner = prediction?.ToString();
                dto.StatusResult = DetermineAppointmentStatusResult(dto.StatusInner);
            }

            // Get AppointmentId to find the appointment details
            if (values.TryGetValue("AppointmentId", out var apptIdObj) && TryParseGuid(apptIdObj, out var appointmentId))
            {
                var appointment = await _arpaContext.Appointments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == appointmentId);

                if (appointment != null)
                {
                    // Format: "DD.MM. HH:mm Name"
                    dto.TargetName = $"{appointment.StartTime:dd.MM. HH:mm} {appointment.Name}";
                    dto.TargetId = appointment.Id;
                }
                else
                {
                    dto.TargetName = "Termin";
                    dto.TargetId = appointmentId;
                }
            }
            else
            {
                dto.TargetName = "Termin";
            }

            return dto;
        }

        private static string DetermineStatusResult(string statusInner)
        {
            if (string.IsNullOrEmpty(statusInner))
            {
                return "Pending";
            }

            return statusInner.ToUpperInvariant() switch
            {
                "ACCEPTANCE" => "Acceptance",
                "REFUSAL" or "REHEARSALSONLY" => "Refusal",
                _ => "Pending"
            };
        }

        private static string DetermineAppointmentStatusResult(string prediction)
        {
            if (string.IsNullOrEmpty(prediction))
            {
                return "Pending";
            }

            return prediction.ToUpperInvariant() switch
            {
                "YES" => "Acceptance",
                "NO" => "Refusal",
                _ => "Pending"
            };
        }

        private static bool TryParseGuid(object value, out Guid result)
        {
            result = Guid.Empty;

            if (value == null)
            {
                return false;
            }

            if (value is Guid guid)
            {
                result = guid;
                return true;
            }

            if (value is JsonElement jsonElement)
            {
                return Guid.TryParse(jsonElement.GetString(), out result);
            }

            return Guid.TryParse(value.ToString(), out result);
        }
    }
}
