using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class ResetPassword
    {
        public class Command : IRequest
        {
            public string UsernameOrEmail { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager)
            {
                RuleFor(c => c.UsernameOrEmail)
                    .MustAsync(async (username, cancellation) => await userManager.FindUserByUsernameOrEmailAsync(username) != null)
                    .OnFailure(request => throw new NotFoundException(nameof(User), nameof(Command.UsernameOrEmail), request));
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(
                ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindUserByUsernameOrEmailAsync(request.UsernameOrEmail);

                IdentityResult resetPasswordResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

                if (resetPasswordResult.Succeeded)
                {
                    return Unit.Value;
                }

                throw new IdentityException("Problem resetting password", resetPasswordResult.Errors);
            }
        }
    }
}
