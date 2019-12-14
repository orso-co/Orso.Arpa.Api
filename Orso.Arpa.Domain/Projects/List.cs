using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Projects
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

            public Task<IImmutableList<Project>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<Project> projects = _repository.GetAll<Project>();

                if (!request.IncludeCompleted)
                {
                    projects = projects.Where(p => !p.IsCompleted);
                }

                return Task.FromResult(projects.ToImmutableList() as IImmutableList<Project>);
            }
        }
    }
}
