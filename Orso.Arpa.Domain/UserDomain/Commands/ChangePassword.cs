using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class ChangePassword
    {
        public class Command : IRequest
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager,
                IUserAccessor userAccessor)
            {
                _ = RuleFor(c => c.CurrentPassword)
                    .MustAsync(async (oldPassword, cancellation) =>
                    {
                        User user = await userAccessor.GetCurrentUserAsync(cancellation);
                        return await userManager.CheckPasswordAsync(user, oldPassword);
                    })
                    .WithMessage("Incorrect password supplied");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(
                IUserAccessor userAccessor,
                ArpaUserManager userManager)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userAccessor.GetCurrentUserAsync(cancellationToken);

                IdentityResult result = await _userManager.ChangePasswordAsync(
                    user,
                    request.CurrentPassword,
                    request.NewPassword);

                return result.Succeeded ? Unit.Value : throw new IdentityException("Problem changing password", result.Errors);
            }
        }
    }
}
