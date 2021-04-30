using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class Details
    {
        public class Query : IRequest<User> { }

        public class QueryById : IRequest<User>
        {
            public QueryById(Guid id)
            {
                Id = id;
            }

            private Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly IUserAccessor _userAccessor;

            public Handler(
                IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userAccessor.GetCurrentUserAsync();
            }
        }
    }
}
