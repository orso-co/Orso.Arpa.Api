using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range);

        Task<AppointmentDto> GetAsync(Guid id);

        Task<AppointmentDto> CreateAsync(AppointmentCreateDto appointmentCreateDto);

        Task ModifyAsync(AppointmentModifyDto appointmentModifyDto);

        Task RemoveRoomAsync(RemoveRoomDto removeRoomDto);

        Task AddRoomAsync(AddRoomDto addRoomDto);

        Task AddProjectAsync(AddProjectDto addProjectDto);

        Task AddSectionAsync(AddSectionDto addSectionDto);

        Task RemoveSectionAsync(RemoveSectionDto removeSectionDto);

        Task RemoveProjectAsync(RemoveProjectDto removeProjectDto);

        Task SetVenueAsync(SetVenueDto setVenueDto);

        Task SetDatesAsync(SetDatesDto setDatesDto);
    }
}
