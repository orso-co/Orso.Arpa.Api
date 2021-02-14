using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Users
{
    public static class UserRoles
    {
        public class Query : IRequest<IEnumerable<string>>
        {
            public Query(User user)
            {
                User = user;
            }

            public User User { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<string>>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(
                ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userManager.GetRolesAsync(request.User);
            }
        }
    }
}
