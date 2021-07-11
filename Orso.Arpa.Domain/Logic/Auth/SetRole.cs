using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class SetRole
    {
        public class Command : IRequest<bool>
        {
            public string Username { get; set; }
            public IEnumerable<string> RoleNames { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager,
                RoleManager<Role> roleManager)
            {
                RuleFor(c => c.Username)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                    .WithErrorCode("404")
                    .WithMessage("User could not be found.");
                RuleForEach(c => c.RoleNames)
                    .MustAsync(async (roleName, cancellation) => await roleManager.RoleExistsAsync(roleName))
                    .WithErrorCode("404")
                    .WithMessage("Role could not be found.");
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

                IList<string> currentRoles = await _userManager.GetRolesAsync(user);
                var isNewUser = currentRoles.Count == 0;

                if (!isNewUser)
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }
                await _userManager.AddToRolesAsync(user, request.RoleNames);

                return isNewUser;
            }
        }
    }
}
