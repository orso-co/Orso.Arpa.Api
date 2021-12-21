using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Domain.Logic.Users
{
    public static class Delete
    {
        public class Command : IRequest
        {
            public Command(string userName)
            {
                UserName = userName;
            }

            public string UserName { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), nameof(Command.UserName));
                }

                if (await CheckIfLastAdminWillBeRemovedAsync(user.Id))
                {
                    throw new ValidationException(new ValidationFailure[] { new ValidationFailure(nameof(request.UserName), "The operation is not allowed because it would remove the last administrator") });
                }

                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }
                throw new IdentityException("Problem deleting user", result.Errors);
            }

            private async Task<bool> CheckIfLastAdminWillBeRemovedAsync(Guid userId)
            {
                IList<User> adminUsers = await _userManager.GetUsersInRoleAsync(RoleNames.Admin);
                return adminUsers.Count == 1 && adminUsers.First().Id.Equals(userId);
            }
        }
    }
}
