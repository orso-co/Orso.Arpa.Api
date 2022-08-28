using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentListDto>> GetAsync(DateTime? date, DateRange range);

        Task<AppointmentDto> GetByIdAsync(Guid id, bool includeParticipations);

        Task<AppointmentDto> CreateAsync(AppointmentCreateDto appointmentCreateDto);

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
    }
}
