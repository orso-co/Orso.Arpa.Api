using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class GetForProject
    {
        public class Query : IRequest<IOrderedQueryable<ProjectParticipation>>
        {
            public Guid ProjectId { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ProjectId)
                    .EntityExists<Query, Project>(arpaContext, nameof(Query.ProjectId));
            }
        }

        public class Handler : IRequestHandler<Query, IOrderedQueryable<ProjectParticipation>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<IOrderedQueryable<ProjectParticipation>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_arpaContext.ProjectParticipations
                    .AsQueryable()
                    .Where(pp => pp.ProjectId == request.ProjectId)
                    .OrderBy(pp => pp.MusicianProfile.Person.Surname));
            }
        }
    }
}
