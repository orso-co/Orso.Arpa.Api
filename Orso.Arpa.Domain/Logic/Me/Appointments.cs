using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class Appointments
    {
        public class Query : IRequest<IEnumerable<Appointment>>
        {
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;
            }

            public int? Limit { get; }
            public int? Offset { get; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Appointment>>
        {
            private readonly IUserAccessor _userAccessor;

            public Handler(
                IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
            }

            public async Task<IEnumerable<Appointment>> Handle(Query request, CancellationToken cancellationToken)
            {
                User user = await _userAccessor.GetCurrentUserAsync();
                IOrderedEnumerable<Appointment> appointments = user.Person.MusicianProfiles
                    .SelectMany(mp => mp.ProjectParticipations)
                    .Select(pp => pp.Project)
                    .SelectMany(p => p.ProjectAppointments)
                    .Select(pa => pa.Appointment)
                    .OrderByDescending(p => p.StartTime);

                return appointments
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? appointments.Count());
            }
        }
    }
}
