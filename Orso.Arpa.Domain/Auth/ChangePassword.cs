using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Auth
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
                UserManager<User> userManager,
                IUserAccessor userAccessor)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.CurrentPassword)
                    .MustAsync(async (oldPassword, cancellation) =>
                    {
                        User user = await userAccessor.GetCurrentUserAsync();
                        return await userManager.CheckPasswordAsync(user, oldPassword);
                    })
                    .WithMessage("User does not exist or wrong password supplied");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<User> _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(
                IUserAccessor userAccessor,
                UserManager<User> userManager)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userAccessor.GetCurrentUserAsync();

                IdentityResult result = await _userManager.ChangePasswordAsync(
                    user,
                    request.CurrentPassword,
                    request.NewPassword);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new IdentityException("Problem changing password", result.Errors);
            }
        }
    }
}
