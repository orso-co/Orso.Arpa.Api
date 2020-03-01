using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class AppointmentList
    {
        public class Query : IRequest<Tuple<IEnumerable<Appointment>, int>>
        {
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;
            }

            public int? Limit { get; }
            public int? Offset { get; }
        }

        public class Handler : IRequestHandler<Query, Tuple<IEnumerable<Appointment>, int>>
        {
            private readonly IUserAccessor _userAccessor;

            public Handler(
                IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
            }

            public async Task<Tuple<IEnumerable<Appointment>, int>> Handle(Query request, CancellationToken cancellationToken)
            {
                User user = await _userAccessor.GetCurrentUserAsync();
                IOrderedEnumerable<Appointment> appointments = user.Person.MusicianProfiles
                    .SelectMany(mp => mp.ProjectParticipations)
                    .Select(pp => pp.Project)
                    .SelectMany(p => p.ProjectAppointments)
                    .Select(pa => pa.Appointment)
                    .OrderByDescending(p => p.StartTime);

                var count = appointments.Count();

                return new Tuple<IEnumerable<Appointment>, int>(appointments
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? count), count);
            }
        }
    }
}
