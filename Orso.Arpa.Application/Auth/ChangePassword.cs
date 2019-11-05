using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Validators;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Auth
{
    public static class ChangePassword
    {
        public class Command : IRequest
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(
                UserManager<User> userManager,
                IUserAccessor userAccessor)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.NewPassword)
                    .Password();
                RuleFor(c => c.CurrentPassword)
                    .NotEmpty()
                    .MustAsync(async (oldPassword, cancellation) =>
                    {
                        User user = await userAccessor.GetCurrentUserAsync();
                        if (user == null)
                        {
                            return false;
                        }

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

                throw new Exception($"Problem changing password: {string.Join("; ", result.Errors)}");
            }
        }
    }
}
