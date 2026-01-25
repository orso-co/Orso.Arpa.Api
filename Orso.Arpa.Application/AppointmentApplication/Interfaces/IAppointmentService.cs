using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.AppointmentApplication.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentListDto>> GetAsync(DateTime? date, DateRange range);

        Task<AppointmentDto> GetByIdAsync(Guid id, bool includeParticipations);

        Task<AppointmentDto> CreateAsync(AppointmentCreateDto appointmentCreateDto);
        Task<AppointmentDto> CopyAsync(AppointmentCopyDto appointmentCopyDto);

        Task ModifyAsync(AppointmentModifyDto appointmentModifyDto);

        Task RemoveRoomAsync(AppointmentRemoveRoomDto removeRoomDto);

        Task AddRoomAsync(AppointmentAddRoomDto addRoomDto);

        Task<AppointmentDto> AddProjectAsync(AppointmentAddProjectDto addProjectDto, bool includeParticipations);

        Task<AppointmentDto> AddSectionAsync(AppointmentAddSectionDto addSectionDto, bool includeParticipations);

        Task<AppointmentDto> RemoveSectionAsync(AppointmentRemoveSectionDto removeSectionDto, bool includeParticipations);

        Task<AppointmentDto> RemoveProjectAsync(AppointmentRemoveProjectDto removeProjectDto, bool includeParticipations);

        Task SetVenueAsync(AppointmentSetVenueDto setVenueDto);

        Task<AppointmentDto> SetDatesAsync(AppointmentSetDatesDto setDatesDto);

        Task DeleteAsync(Guid id);

        Task SetParticipationResultAsync(AppointmentParticipationSetResultDto setParticipationResult);
        Task SetParticipationPredictionAsync(AppointmentParticipationSetPredictionDto setParticipationPrediction);
        Task SetParticipationCommentByStaffAsync(AppointmentParticipationSetCommentByStaffDto setCommentByStaff);
        Task SendAppointmentChangedNotificationAsync(SendAppointmentChangedNotificationDto sendAppointmentChangedNotificationDto);

        Task<string> ExportAppointmentsToIcsAsync();

        Task AddPrioritizedPieceAsync(Guid appointmentId, Guid setlistPieceId);
        Task RemovePrioritizedPieceAsync(Guid appointmentId, Guid setlistPieceId);
    }
}
