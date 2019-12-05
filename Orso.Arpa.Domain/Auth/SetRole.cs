using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Domain.Auth
{
    public static class SetRole
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }
            public string RoleName { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.Users
                    .Include(u => u.Person)
                    .SingleOrDefaultAsync(u => u.NormalizedUserName == _userManager.NormalizeKey(request.UserName));

                foreach (var role in RoleNames.Roles)
                {
                    if (request.RoleName.Equals(role, StringComparison.InvariantCultureIgnoreCase))
                    {
                        await AddRoleToUserAsync(user, role);
                    }
                    else
                    {
                        await RemoveRoleFromUserAsync(user, role);
                    }
                }

                return Unit.Value;
            }

            private async Task RemoveRoleFromUserAsync(User user, string role)
            {
                if (await _userManager.IsInRoleAsync(user, role))
                {
                    IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        throw new IdentityException("Problem removing user from role", result.Errors);
                    }
                }
            }

            private async Task AddRoleToUserAsync(User user, string role)
            {
                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        throw new IdentityException("Problem adding user to role", result.Errors);
                    }
                }
            }
        }
    }
}
