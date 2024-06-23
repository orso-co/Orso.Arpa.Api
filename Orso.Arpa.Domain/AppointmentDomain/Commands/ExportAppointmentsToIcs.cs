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
        private readonly IArpaContext _context;

        public Handler(IArpaContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            IEnumerable<Appointment> appointments = await _context.Appointments
                .Include(a => a.Category)
                .Include(a => a.Venue)
                .ToListAsync(cancellationToken);

            var calendar = new Calendar();

            string timeZoneId = "Europe/Berlin";
            var timeZone = new VTimeZone(timeZoneId);
            calendar.AddTimeZone(timeZone);

            IEnumerable<CalendarEvent> events = appointments.Select(a => new CalendarEvent
            {
                Summary = a.Name,
                Description = (a.Category != null ? a.Category.SelectValue.Name : "-") + " - " +
                              (string.IsNullOrEmpty(a.PublicDetails) ? "-" : a.PublicDetails) +
                              " - " +
                              (string.IsNullOrEmpty(a.InternalDetails) ? "-" : a.InternalDetails),
                Location = a.Venue != null ? a.Venue.Address.City : "-",
                Start = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(a.StartTime)),
                End = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(a.EndTime))
            });

            foreach (CalendarEvent calendarEvent in events) calendar.Events.Add(calendarEvent);

            var serializer = new CalendarSerializer();
            return serializer.SerializeToString(calendar);
        }
    }
}
