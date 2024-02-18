using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Queries
{
    public static class GetProfilePictureFileName
    {
        public class Query : IRequest<string>
        {
            public Query(Guid personId)
            {
                PersonId = personId;
            }

            public Guid PersonId { get; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _arpaContext.Set<Person>().FindAsync([request.PersonId], cancellationToken))?.ProfilePictureFileName;
            }
        }
    }
}
