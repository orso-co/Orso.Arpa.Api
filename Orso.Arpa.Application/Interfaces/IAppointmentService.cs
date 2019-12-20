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

        Task RemoveRoomAsync(Guid id, Guid roomId);

        Task AddRoomAsync(Guid id, Guid roomId);
        Task AddProjectAsync(Guid id, Guid projectId);
        Task AddRegisterAsync(Guid id, Guid registerId);
        Task RemoveRegisterAsync(Guid id, Guid registerId);
        Task RemoveProjectAsync(Guid id, Guid projectId);
    }
}
