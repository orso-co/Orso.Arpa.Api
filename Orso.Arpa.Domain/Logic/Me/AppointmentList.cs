using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class AppointmentList
    {
        public class Query : IRequest<Tuple<IQueryable<Appointment>, int>>
        {
            public Query(int? limit, int? offset, IEnumerable<ITree<Section>> sectionTree, Person person)
            {
                Limit = limit;
                Offset = offset;
                SectionTree = sectionTree;
                Person = person;
            }

            public int? Limit { get; }
            public int? Offset { get; }
            public IEnumerable<ITree<Section>> SectionTree { get; }
            public Person Person { get; }
        }

        public class Handler : IRequestHandler<Query, Tuple<IQueryable<Appointment>, int>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Tuple<IQueryable<Appointment>, int>> Handle(Query request, CancellationToken cancellationToken)
            {
                IList<Guid> appointmentIds = await _arpaContext.GetAppointmentIdsForPerson(request.Person.Id).Select(a => a.Id).ToListAsync();
                var count = appointmentIds.Count;

                IQueryable<Appointment> appointmentsForUser = _arpaContext.Appointments.AsQueryable()
                    .Where(appointment => appointmentIds.Contains(appointment.Id))
                    .OrderByDescending(p => p.StartTime)
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? count);

                return new Tuple<IQueryable<Appointment>, int>(appointmentsForUser, count);
            }
        }
    }
}
