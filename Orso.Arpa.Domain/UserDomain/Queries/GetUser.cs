using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
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
            private readonly ArpaUserManager _arpaUserManager;

            public Handler(
                ArpaUserManager arpaUserManager)
            {
                _arpaUserManager = arpaUserManager;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                User user = await _arpaUserManager.FindByIdAsync(request.Id, cancellationToken);
                if (user is null)
                {
                    throw new NotFoundException(nameof(User), nameof(Query.Id));
                }
                return user;
            }
        }
    }
}
