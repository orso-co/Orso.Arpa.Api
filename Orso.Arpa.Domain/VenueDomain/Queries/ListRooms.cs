using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Domain.VenueDomain.Queries
{
    public static class ListRooms
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
                    .Venues.FindAsync(new object[] { request.Id }, cancellationToken))
                    .Rooms
                    .ToImmutableList();
            }
        }
    }
}
