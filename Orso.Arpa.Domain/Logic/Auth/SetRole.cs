using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class SetRole
    {
        public class Command : IRequest<bool>
        {
            public string Username { get; set; }
            public string RoleName { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager,
                RoleManager<Role> roleManager)
            {
                RuleFor(c => c.Username)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                    .OnFailure(request => throw new NotFoundException(nameof(User), nameof(Command.Username), request));
                RuleFor(c => c.RoleName)
                    .MustAsync(async (roleName, cancellation) => string.IsNullOrEmpty(roleName) || await roleManager.RoleExistsAsync(roleName))
                    .OnFailure(request => throw new NotFoundException(nameof(Role), nameof(Command.RoleName), request));
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.Users
                    .Include(u => u.Person)
                    .SingleOrDefaultAsync(u => u.NormalizedUserName == _userManager.NormalizeName(request.Username), cancellationToken);

                var isNewUser = (await _userManager.GetRolesAsync(user)).Count == 0;

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

                return isNewUser;
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
