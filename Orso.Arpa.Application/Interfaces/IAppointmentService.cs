using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.AppointmentParticipations;
using Orso.Arpa.Application.Logic.Appointments;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range);

        Task<AppointmentDto> GetAsync(Guid id);

        Task<AppointmentDto> CreateAsync(Create.Dto appointmentCreateDto);

        Task ModifyAsync(Modify.Dto appointmentModifyDto);

        Task RemoveRoomAsync(RemoveRoom.Dto removeRoomDto);

        Task AddRoomAsync(AddRoom.Dto addRoomDto);

        Task AddProjectAsync(AddProject.Dto addProjectDto);

        Task AddSectionAsync(AddSection.Dto addSectionDto);

        Task RemoveSectionAsync(RemoveSection.Dto removeSectionDto);

        Task RemoveProjectAsync(RemoveProject.Dto removeProjectDto);

        Task SetVenueAsync(SetVenue.Dto setVenueDto);

        Task SetDatesAsync(SetDates.Dto setDatesDto);

        Task DeleteAsync(Guid id);

        Task SetParticipationResultAsync(SetResult.Dto setParticipationResult);
    }
}
