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
    }
}
