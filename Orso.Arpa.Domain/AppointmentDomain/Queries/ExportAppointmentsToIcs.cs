using System;
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
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Domain.AppointmentDomain.Queries;

public class ExportAppointmentsToIcs
{
    public class Query : IRequest<string>
    {
        public Guid? PersonId { get; set; }
        public Guid? ProjectId { get; set; }
        public string CalendarName { get; set; } = "ARPA Termine";
    }

    public class Handler : IRequestHandler<Query, string>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext context)
        {
            _arpaContext = context;
        }

        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            IQueryable<Model.Appointment> query = _arpaContext.Appointments;

            // Filter by person (eligible appointments)
            List<Guid> eligibleIds = null;
            if (request.PersonId.HasValue)
            {
                eligibleIds = await _arpaContext
                    .GetAppointmentIdsForPerson(request.PersonId.Value)
                    .Select(r => r.Id)
                    .ToListAsync(cancellationToken);

                query = query.Where(a => eligibleIds.Contains(a.Id));
            }

            // Filter by project
            if (request.ProjectId.HasValue)
            {
                query = query.Where(a =>
                    a.ProjectAppointments.Any(pa => pa.ProjectId == request.ProjectId.Value));
            }

            var appointmentList = await query
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    CategoryName = a.Category != null ? a.Category.SelectValue.Name : "-",
                    a.PublicDetails,
                    a.InternalDetails,
                    Location = a.Venue != null ? a.Venue.Address.City : "-",
                    a.StartTime,
                    a.EndTime,
                    a.Type
                })
                .ToListAsync(cancellationToken);

            var events = appointmentList.Select(a =>
            {
                var calEvent = new CalendarEvent
                {
                    Uid = a.Id.ToString(),
                    Summary = a.Name,
                    Description = a.CategoryName + " | " +
                                  (string.IsNullOrEmpty(a.PublicDetails) ? "-" : a.PublicDetails) +
                                  " | " +
                                  (string.IsNullOrEmpty(a.InternalDetails)
                                      ? "-"
                                      : a.InternalDetails),
                    Location = a.Location,
                    Start = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(a.StartTime), "Europe/Berlin"),
                    End = new CalDateTime(DateTimeExtensions.ConvertToLocalTimeBerlin(a.EndTime), "Europe/Berlin")
                };

                if (a.Type == AppointmentType.InfoOnly)
                {
                    calEvent.Transparency = TransparencyType.Transparent;
                }

                return calEvent;
            }).ToList();

            var calendar = new Calendar();
            calendar.Properties.Add(new CalendarProperty("X-WR-CALNAME", request.CalendarName));

            string timeZoneId = "Europe/Berlin";
            var tzInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var timeZone = VTimeZone.FromSystemTimeZone(tzInfo);
            calendar.AddTimeZone(timeZone);

            calendar.Events.AddRange(events);

            var serializer = new CalendarSerializer();
            return serializer.SerializeToString(calendar);
        }
    }
}
