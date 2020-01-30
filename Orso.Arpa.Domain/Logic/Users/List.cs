using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Logic.Users
{
    public static class List
    {
        public class Query : IRequest<IEnumerable<User>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<User>>
        {
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager)
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
