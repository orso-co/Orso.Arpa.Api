using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.MyProjects;

public static class List
{
    public class Query : IRequest<IImmutableList<ProjectParticipation>>
    {
        public Guid PersonId { get; set; }
    }

    public class Handler : IRequestHandler<Query, IImmutableList<ProjectParticipation>>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public Task<IImmutableList<ProjectParticipation>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_arpaContext.ProjectParticipations.Where(p =>
                !p.Project.IsCompleted &&
                p.InvitationStatusId.HasValue &&
                _arpaContext.MusicianProfiles.Where(mp => mp.PersonId == request.PersonId && (!p.Project.StartDate.HasValue || mp.IsDeactivated(p.Project.StartDate.Value))).Contains(p.MusicianProfile)).ToImmutableList() as IImmutableList<ProjectParticipation> );
        }
    }
}
