using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Queries
{
    public static class GetUserByPersonId
    {
        public class Query : IRequest<User>
        {
            public Query(Guid personId)
            {
                PersonId = personId;
            }

            public Guid PersonId { get; set; }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly IArpaContext _arpaContext;


            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _arpaContext.Set<User>().FirstOrDefaultAsync(u => u.PersonId == request.PersonId, cancellationToken);
            }
        }
    }
}