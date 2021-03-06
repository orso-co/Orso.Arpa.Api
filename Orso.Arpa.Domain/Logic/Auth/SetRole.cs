using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Resources.Cultures;

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
                RoleManager<Role> roleManager,
                IStringLocalizer<DomainResource>  localizer)
            {
                RuleFor(c => c.Username)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                    .WithMessage(localizer["The user could not be found"]);
                RuleForEach(c => c.RoleNames)
                    .MustAsync(async (roleName, cancellation) => await roleManager.RoleExistsAsync(roleName))
                    .WithMessage(localizer["The role '{PropertyValue}' could not be found"]);
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
