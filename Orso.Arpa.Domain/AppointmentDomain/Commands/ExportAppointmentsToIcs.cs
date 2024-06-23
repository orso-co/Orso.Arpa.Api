using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands;

public class ExportAppointmentsToIcs
{
    public class Command : IRequest<string>
    {
    }

    public class Handler : IRequestHandler<Command, string>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext context)
        {
            _arpaContext = context;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            List<CalendarEvent> events = await _arpaContext.Appointments.Select(a => new CalendarEvent
            {
                Summary = a.Name,
                Description = (a.Category != null ? a.Category.SelectValue.Name : "-") + " - " +
                              (string.IsNullOrEmpty(a.PublicDetails) ? "-" : a.PublicDetails) +
                              " - " +
                              (string.IsNullOrEmpty(a.InternalDetails) ? "-" : a.InternalDetails),
                Location = a.Venue != null ? a.Venue.Address.City : "-",
                Start = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(a.StartTime)),
                End = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(a.EndTime))
            }).ToListAsync(cancellationToken);

            var calendar = new Calendar();

            string timeZoneId = "Europe/Berlin";
            var timeZone = new VTimeZone(timeZoneId);
            calendar.AddTimeZone(timeZone);

            calendar.Events.AddRange(events);

            var serializer = new CalendarSerializer();
            return serializer.SerializeToString(calendar);
        }
    }
}
