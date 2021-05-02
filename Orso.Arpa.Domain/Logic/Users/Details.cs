using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Users
{
    public static class Details
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
