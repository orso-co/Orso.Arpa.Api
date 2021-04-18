using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class AppointmentList
    {
        public class Query : IRequest<Tuple<IEnumerable<Appointment>, int>>
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

        public class Handler : IRequestHandler<Query, Tuple<IEnumerable<Appointment>, int>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<Tuple<IEnumerable<Appointment>, int>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<Appointment> appointmentsForUser = _arpaContext.Appointments.AsQueryable()
                    .Where(appointment => _arpaContext.AppointmentsForUser
                        .AsQueryable()
                        .Where(u => u.PersonId == request.Person.Id || u.PersonId == null)
                        .Select(afu => afu.Id)
                        .Contains(appointment.Id));

                var count = appointmentsForUser.Count();

                return Task.FromResult(new Tuple<IEnumerable<Appointment>, int>(appointmentsForUser
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? count), count));
            }
        }
    }
}
