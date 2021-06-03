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

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
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
                List<MusicianProfile> profiles = await _arpaContext.MusicianProfiles
                    .AsSplitQuery()
                    .AsAsyncEnumerable()
                    .Where(p => IsMusicianProfileInProject(p, request.Appointment)
                        && IsMusicianProfileInSection(p, request.Appointment, request.SectionTree))
                    .ToListAsync();

                return from p in profiles
                       group p by p.Person into g
                       select new PersonGrouping
                       {
                           Person = g.Key,
                           Profiles = g,
                           Participation = request.Appointment.AppointmentParticipations.FirstOrDefault(ap => ap.PersonId == g.Key.Id)
                       };
            }

            private bool IsMusicianProfileInProject(MusicianProfile musicianProfile, Appointment appointment)
            {
                if (appointment.ProjectAppointments.Count == 0)
                {
                    return true;
                }
                return musicianProfile.ProjectParticipations
                    .Select(pp => pp.ProjectId)
                    .Intersect(appointment.ProjectAppointments.Select(pa => pa.ProjectId))
                    .Any();
            }

            private bool IsMusicianProfileInSection(MusicianProfile musicianProfile, Appointment appointment, IEnumerable<ITree<Section>> sectionTree)
            {
                if (appointment.SectionAppointments.Count == 0)
                {
                    return true;
                }
                return sectionTree.FirstOrDefault(st => st.Data.Id == musicianProfile.InstrumentId)
                    .GetParents()
                    .Select(section => section.Id)
                    .Union(new Guid[] { musicianProfile.InstrumentId })
                    .Intersect(appointment.SectionAppointments.Select(sa => sa.SectionId))
                    .Any();
            }
        }
    }
}
