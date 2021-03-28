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

            public async Task<Tuple<IEnumerable<Appointment>, int>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Appointment> appointments = await _arpaContext.Appointments
                    .AsAsyncEnumerable()
                    .Where(appointment =>
                        IsPersonInProject(appointment, request.Person) &&
                        IsPersonInSection(appointment, request.Person, request.SectionTree))
                    .OrderByDescending(p => p.StartTime)
                    .ToListAsync();

                var count = appointments.Count();

                return new Tuple<IEnumerable<Appointment>, int>(appointments
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? count), count);
            }

            private static bool IsPersonInSection(Appointment appointment, Person person, IEnumerable<ITree<Section>> sectionTree)
            {
                if(appointment.SectionAppointments.Count == 0)
                {
                    return true;
                }
                return person.MusicianProfiles
                    .Select(mp => sectionTree.FirstOrDefault(t => t.Data.Id == mp.SectionId))
                    .SelectMany(node => node.GetParents())
                    .Select(section => section.Id)
                    .Intersect(appointment.SectionAppointments.Select(sa => sa.SectionId))
                    .Any();
            }

            private static bool IsPersonInProject(Appointment appointment, Person person)
            {
                if(appointment.ProjectAppointments.Count == 0)
                {
                    return true;
                }
                return person.MusicianProfiles
                    .SelectMany(profile => profile.ProjectParticipations)
                    .Select(participation => participation.ProjectId)
                    .Intersect(appointment.ProjectAppointments.Select(pa => pa.ProjectId))
                    .Any();
            }
        }
    }
}
