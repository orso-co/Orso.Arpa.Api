using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Users
{
    public static class Roles
    {
        public class Query : IRequest<IEnumerable<string>>
        {
            public User User { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<string>>
        {
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            // ToDo: Test
            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _userManager.GetRolesAsync(request.User);
            }
        }
    }
}
