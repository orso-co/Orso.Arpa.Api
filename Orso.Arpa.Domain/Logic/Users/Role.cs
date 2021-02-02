using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Users
{
    public static class Role
    {
        public class Query : IRequest<Entities.Role>
        {
            public Query(User user)
            {
                User = user;
            }

            public User User { get; set; }
        }

        public class Handler : IRequestHandler<Query, Entities.Role>
        {
            private readonly ArpaUserManager _userManager;
            private readonly RoleManager<Entities.Role> _roleManager;

            public Handler(
                ArpaUserManager userManager,
                RoleManager<Entities.Role> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Entities.Role> Handle(Query request, CancellationToken cancellationToken)
            {
                var roleName = (await _userManager.GetRolesAsync(request.User)).FirstOrDefault();

                if (string.IsNullOrEmpty(roleName))
                {
                    return null;
                }

                return await _roleManager.FindByNameAsync(roleName);
            }
        }
    }
}
