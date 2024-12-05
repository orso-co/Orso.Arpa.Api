using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Util;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.AppointmentDomain.Queries
{
    public static class ListMyAppointmentRange
    {
        public class Query(DateTime? dateTime, DateRange dateRange, Guid personId) : IRequest<IList<Appointment>>
        {
            public DateTime? DateTime { get; } = dateTime;
            public DateRange DateRange { get; } = dateRange;
            public Guid PersonId { get; } = personId;
        }

        public class Handler(IArpaContext arpaContext) : IRequestHandler<Query, IList<Appointment>>
        {
            private readonly IArpaContext _arpaContext = arpaContext;

            public async Task<IList<Appointment>> Handle(Query request, CancellationToken cancellationToken)
            {
                DateTime date = request.DateTime ?? DateTime.Today;

                DateTime rangeStartTime = DateHelper.GetStartTime(date, request.DateRange);
                DateTime rangeEndTime = DateHelper.GetEndTime(date, request.DateRange);

                List<Appointment> appointments = await _arpaContext.Appointments.AsQueryable().Where(a => (
                    a.EndTime <= rangeEndTime && a.EndTime >= rangeStartTime)
                    || (a.EndTime > rangeEndTime && a.StartTime <= rangeEndTime)).ToListAsync(cancellationToken);
                var appointmentsToReturn = new List<Appointment>();

                return [.. appointments.Where(a => _arpaContext.IsPersonEligibleForAppointment(request.PersonId, a.Id))];
            }
        }
    }
}
