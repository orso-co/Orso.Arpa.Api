using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using MediatR;
using Orso.Arpa.Misc.Extensions;
using Orso.Arpa.Application.AppointmentApplication.Interfaces;
using Orso.Arpa.Application.AppointmentApplication.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public class ExportAppointmentsToIcs
    {
        public class Command : IRequest<string>
        {
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly IAppointmentService _appointmentService;

            public Handler(IAppointmentService appointmentService)
            {
                _appointmentService = appointmentService;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                IEnumerable<AppointmentListDto> appointments = await _appointmentService.GetAsync(null, null);

                var calendar = new Calendar();

                string timeZoneId = "Europe/Berlin";
                var timeZone = new VTimeZone(timeZoneId);
                calendar.AddTimeZone(timeZone);

                foreach (AppointmentListDto appointment in appointments)
                {
                    var calendarEvent = new CalendarEvent
                    {
                        Summary = appointment.Name,
                        Description = appointment.Category,
                        Location = appointment.City,
                        Start = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(appointment.StartTime)),
                        End = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(appointment.EndTime))
                    };

                    calendar.Events.Add(calendarEvent);
                }

                var serializer = new CalendarSerializer();
                return serializer.SerializeToString(calendar);
            }
        }
    }
}
