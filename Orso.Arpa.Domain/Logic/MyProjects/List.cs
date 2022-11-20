using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.MyProjects;

public static class List
{
    public class MyProjectGrouping
    {
        public Project Project { get; set; }
        public IEnumerable<ProjectParticipation> ProjectParticipations { get; set; }
    }

    public class Query : IRequest<IEnumerable<MyProjectGrouping>>
    {
        public Guid PersonId { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<MyProjectGrouping>>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<IEnumerable<MyProjectGrouping>> Handle(Query request, CancellationToken cancellationToken)
        {
            // ToDo: In DB Query auslagern (Reinhold)
            List<MusicianProfile> musicianProfiles = await _arpaContext.MusicianProfiles.Where(mp => mp.PersonId == request.PersonId).ToListAsync(cancellationToken: cancellationToken);
            List<Guid> musicianProfileIds = musicianProfiles.ConvertAll(mp => mp.Id);

            List<ProjectParticipation> projectParticipations = await _arpaContext.ProjectParticipations
                .Where(p =>
                    !p.Project.IsCompleted &&
                    p.InvitationStatus.HasValue &&
                    musicianProfileIds.Contains(p.MusicianProfileId))
                .ToListAsync(cancellationToken);

            var selected = projectParticipations
                .Where(p => musicianProfiles.Any(mp => !p.Project.StartDate.HasValue || !mp.IsDeactivated(p.Project.StartDate.Value)))
                .OrderByDescending(pp => pp.Project.StartDate)
                .ToList();

            return from p in selected
                   group p by p.Project into g
                   select new MyProjectGrouping
                   {
                       Project = g.Key,
                       ProjectParticipations = g,
                   };
        }
    }
}
