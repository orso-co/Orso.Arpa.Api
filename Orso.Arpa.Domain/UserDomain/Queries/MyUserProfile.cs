using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Queries
{
    public static class MyUserProfile
    {
        public class Query : IRequest<User> { }

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
                return await _userAccessor.GetCurrentUserAsync(cancellationToken);
            }
        }
    }
}
