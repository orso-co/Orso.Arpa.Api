using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<Region>>
        {
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Region>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public Task<IImmutableList<Region>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll<Region>().ToImmutableList() as IImmutableList<Region>);
            }
        }
    }
}
