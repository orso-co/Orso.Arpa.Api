using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class GetForMusicianProfile
    {
        public class Query : IRequest<IEnumerable<ProjectParticipation>>
        {
            public Guid MusicianProfileId { get; set; }
            public bool IncludeCompletedProjects { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
            {
                CascadeMode = CascadeMode.Stop;

                When(_ => tokenAccessor.UserRoles.Contains(RoleNames.Staff), () =>
                {
                    RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Query, MusicianProfile>(arpaContext, nameof(Query.MusicianProfileId));
                }).Otherwise(() =>
                {
                    RuleFor(c => c.MusicianProfileId)
                        .MustAsync(async (musicianProfileId, cancellation) => await arpaContext
                            .EntityExistsAsync<MusicianProfile>(mp => mp.Id == musicianProfileId && mp.PersonId == tokenAccessor.PersonId, cancellation))
                        .OnFailure(_ => throw new AuthorizationException("This musician profile is not yours. You don't have access to this musician profile."));
                });
            }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ProjectParticipation>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IEnumerable<ProjectParticipation>> Handle(Query request, CancellationToken cancellationToken)
            {
                Expression<Func<ProjectParticipation, bool>> query = request.IncludeCompletedProjects
                    ? pp => pp.MusicianProfileId == request.MusicianProfileId
                    : pp => pp.MusicianProfileId == request.MusicianProfileId && !pp.Project.IsCompleted;

                return await _arpaContext.ProjectParticipations
                    .Where(query)
                    .OrderByDescending(pp => pp.Project.StartDate)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
