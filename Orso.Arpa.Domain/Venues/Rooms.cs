using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Venues
{
    public static class Rooms
    {
        public class Query : IRequest<IImmutableList<Room>>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; private set; }
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Room>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public async Task<IImmutableList<Room>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _repository
                    .GetByIdAsync<Venue>(request.Id))
                    .Rooms
                    .ToImmutableList();
            }
        }
    }
}
