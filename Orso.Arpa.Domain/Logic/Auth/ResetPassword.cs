using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class ResetPassword
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                UserManager<User> userManager)
            {
                RuleFor(c => c.UserName)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                    .OnFailure(request => throw new NotFoundException(nameof(User), nameof(Command.UserName), request));
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.UserName);

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
