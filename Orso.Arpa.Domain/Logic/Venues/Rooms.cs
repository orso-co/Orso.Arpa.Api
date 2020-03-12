using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Venues
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
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IImmutableList<Room>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _arpaContext
                    .Venues.FindAsync(request.Id))
                    .Rooms
                    .ToImmutableList();
            }
        }
    }
}
