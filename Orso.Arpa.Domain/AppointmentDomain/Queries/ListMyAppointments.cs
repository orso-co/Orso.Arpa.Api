using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.AppointmentDomain.Queries
{
    public static class ListMyAppointments
    {
        public class Query : IRequest<(IList<Appointment> userAppointments, int totalCount)>
        {
            public Query(int? limit, int? offset, bool passed, IEnumerable<ITree<Section>> sectionTree, Person person)
            {
                Limit = limit;
                Offset = offset;
                SectionTree = sectionTree;
                Person = person;
                Passed = passed;
            }

            public int? Limit { get; }
            public int? Offset { get; }
            public bool Passed { get; set; }
            public IEnumerable<ITree<Section>> SectionTree { get; }
            public Person Person { get; }
        }

        public class Handler : IRequestHandler<Query, (IList<Appointment> userAppointments, int totalCount)>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IDateTimeProvider _dateTimeProvider;

            public Handler(IArpaContext arpaContext, IDateTimeProvider dateTimeProvider)
            {
                _arpaContext = arpaContext;
                _dateTimeProvider = dateTimeProvider;
            }

            public async Task<(IList<Appointment> userAppointments, int totalCount)> Handle(Query request, CancellationToken cancellationToken)
            {
                IList<Guid> appointmentIds = await _arpaContext.GetAppointmentIdsForPerson(request.Person.Id).Select(a => a.Id).ToListAsync(cancellationToken);
                var count = appointmentIds.Count;

                IQueryable<Appointment> userAppointments = request.Passed
                    ? _arpaContext.Appointments.AsQueryable()
                        .Where(appointment => appointmentIds.Contains(appointment.Id) && appointment.EndTime <= _dateTimeProvider.GetUtcNow())
                        .OrderByDescending(p => p.StartTime)
                    : _arpaContext.Appointments.AsQueryable()
                        .Where(appointment => appointmentIds.Contains(appointment.Id) && appointment.EndTime > _dateTimeProvider.GetUtcNow())
                        .OrderBy(p => p.StartTime);

                return (await userAppointments.Skip(request.Offset ?? 0).Take(request.Limit ?? count).ToListAsync(cancellationToken), userAppointments.Count());
            }
        }
    }
}
