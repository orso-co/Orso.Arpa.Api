using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Queries
{
    public static class GetUser
    {
        public class Query : IRequest<User>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
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
                User user = await _arpaContext.FindAsync<User>(new object[] { request.Id }, cancellationToken);
                return user ?? throw new NotFoundException(nameof(User), nameof(Query.Id));
            }
        }
    }
}
