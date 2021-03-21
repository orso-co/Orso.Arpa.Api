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
        Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range);

        Task<AppointmentDto> GetByIdAsync(Guid id);

        Task<AppointmentDto> CreateAsync(AppointmentCreateDto appointmentCreateDto);

        Task ModifyAsync(AppointmentModifyDto appointmentModifyDto);

        Task RemoveRoomAsync(AppointmentRemoveRoomDto removeRoomDto);

        Task AddRoomAsync(AppointmentAddRoomDto addRoomDto);

        Task<AppointmentDto> AddProjectAsync(AppointmentAddProjectDto addProjectDto);

        Task<AppointmentDto> AddSectionAsync(AppointmentAddSectionDto addSectionDto);

        Task<AppointmentDto> RemoveSectionAsync(AppointmentRemoveSectionDto removeSectionDto);

        Task<AppointmentDto> RemoveProjectAsync(AppointmentRemoveProjectDto removeProjectDto);

        Task SetVenueAsync(AppointmentSetVenueDto setVenueDto);

        Task<AppointmentDto> SetDatesAsync(AppointmentSetDatesDto setDatesDto);

        Task DeleteAsync(Guid id);

        Task SetParticipationResultAsync(AppointmentParticipationSetResultDto setParticipationResult);
    }
}
