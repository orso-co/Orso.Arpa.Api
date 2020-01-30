using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Venues
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<Venue>>
        {
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Venue>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public Task<IImmutableList<Venue>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository
                    .GetAll<Venue>()
                    .OrderBy(v => v.Address.City)
                    .ToImmutableList() as IImmutableList<Venue>);
            }
        }
    }
}
