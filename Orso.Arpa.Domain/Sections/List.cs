using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Sections
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<Section>>
        {
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Section>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public Task<IImmutableList<Section>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository
                    .GetAll<Section>()
                    .OrderBy(r => r.Name)
                    .ToImmutableList() as IImmutableList<Section>);
            }
        }
    }
}
