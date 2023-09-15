using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Domain.UserDomain.Commands
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
                    .WithErrorCode("404")
                    .WithMessage("User could not be found.");
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

                if (resetPasswordResult.Errors.FirstOrDefault()?.Description.Contains("Invalid token") == true)
                {
                    throw new ValidationException(new ValidationFailure[] { new ValidationFailure(nameof(request.Token), "The supplied token is invalid or has expired") });
                }

                throw new IdentityException("Problem resetting password", resetPasswordResult.Errors);
            }
        }
    }
}
