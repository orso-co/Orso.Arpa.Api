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
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Queries
{
    public static class GetForAppointment
    {
        public class PersonGrouping
        {
            public Person Person { get; set; }
            public IEnumerable<MusicianProfile> Profiles { get; set; }
            public AppointmentParticipation Participation { get; set; }
        }

        public class Query : IRequest<IEnumerable<PersonGrouping>>
        {
            public Appointment Appointment { get; set; }
            public IEnumerable<ITree<Section>> SectionTree { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<PersonGrouping>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IEnumerable<PersonGrouping>> Handle(Query request, CancellationToken cancellationToken)
            {
                IList<Guid> musicianProfileIds = await _arpaContext.GetMusicianProfilesForAppointment(request.Appointment.Id)
                        .Select(a => a.Id)
                        .ToListAsync(cancellationToken);

                List<MusicianProfile> profiles = await _arpaContext.MusicianProfiles
                    .AsQueryable()
                    .Where(p => musicianProfileIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                return from p in profiles
                       group p by p.Person into g
                       select new PersonGrouping
                       {
                           Person = g.Key,
                           Profiles = g,
                           Participation = request.Appointment.AppointmentParticipations.FirstOrDefault(ap => ap.PersonId == g.Key.Id)
                       };
            }
        }
    }
}
