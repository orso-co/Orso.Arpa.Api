using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<Project>>
        {
            public bool IncludeCompleted { get; set; }
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Project>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public async Task<IImmutableList<Project>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<Project> projects = _repository.GetAll<Project>();

                if (request.IncludeCompleted)
                {
                    return projects.ToImmutableList();
                }

                return (await projects
                    .ToListAsync())
                    .Where(p => !p.IsCompleted)
                    .ToImmutableList();
            }
        }
    }
}
