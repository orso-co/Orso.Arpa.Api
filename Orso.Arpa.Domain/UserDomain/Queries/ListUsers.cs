using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Queries
{
    public static class ListUsers
    {
        public class Query : IRequest<IEnumerable<User>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<User>>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(
                ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public Task<IEnumerable<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_userManager.Users.Include(u => u.Person).ToList() as IEnumerable<User>);
            }
        }
    }
}
