using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class GetForMusicianProfile
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
                IQueryable<Project> projectQuery = _arpaContext.Projects
                                .Where(p => (!p.IsCompleted || request.IncludeCompletedProjects)
                                    && !p.IsHiddenForPerformers
                                    && !ProjectStatus.Cancelled.Equals(p.Status)
                                    && !p.Children.Any())
                                .OrderByDescending(p => p.StartDate);

                var result = new List<MusicianProfileProjectParticipationGrouping>();

                MusicianProfile musicianProfile = _arpaContext.MusicianProfiles.Find(request.MusicianProfileId);

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
