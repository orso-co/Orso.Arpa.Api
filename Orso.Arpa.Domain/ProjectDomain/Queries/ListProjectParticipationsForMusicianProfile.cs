using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Queries
{
    public static class ListProjectParticipationsForMusicianProfile
    {
        public class Query : IRequest<IEnumerable<MusicianProfileProjectParticipationGrouping>>
        {
            public Guid MusicianProfileId { get; set; }
            public bool IncludeCompletedProjects { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.MusicianProfileId)
                .EntityExists<Query, MusicianProfile>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<MusicianProfileProjectParticipationGrouping>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IEnumerable<MusicianProfileProjectParticipationGrouping>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<Project> projectQuery = _arpaContext
                    .Set<Project>()
                    .Where(p => (!p.IsCompleted || request.IncludeCompletedProjects)
                        && !p.IsHiddenForPerformers
                        && !ProjectStatus.Cancelled.Equals(p.Status)
                        && p.Children.Count.Equals(0))
                    .OrderByDescending(p => p.StartDate);

                var result = new List<MusicianProfileProjectParticipationGrouping>();

                MusicianProfile musicianProfile = await _arpaContext
                    .Set<MusicianProfile>()
                    .FindAsync(keyValues: [request.MusicianProfileId], cancellationToken: cancellationToken);

                foreach (Project project in await projectQuery.ToListAsync(cancellationToken))
                {
                    result.Add(new MusicianProfileProjectParticipationGrouping
                    {
                        Project = project,
                        ProjectParticipation = LoadProjectParticipation(project, musicianProfile),
                        MusicianProfile = musicianProfile
                    });
                }

                return result;
            }

            private static ProjectParticipation LoadProjectParticipation(Project project, MusicianProfile musicianProfile)
            {
                ProjectParticipation existingParticipation = project.ProjectParticipations.FirstOrDefault(pp => pp.MusicianProfileId.Equals(musicianProfile.Id));
                return existingParticipation ?? new ProjectParticipation(project, musicianProfile);
            }
        }
    }
}
