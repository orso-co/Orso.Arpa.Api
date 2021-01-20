using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class ConfirmEmail
    {
        public class Command : IRequest
        {
            public string Token { get; set; }
            public string Email { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                UserManager<User> userManager)
            {
                RuleFor(c => c.Email)
                    .MustAsync(async (email, cancellation) => await userManager.FindByEmailAsync(email) != null)
                    .OnFailure(request => throw new NotFoundException(nameof(User), nameof(Command.Email), request));
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
                User user = await _userManager.FindByEmailAsync(request.Email);

                IdentityResult confirmEmailResult = await _userManager.ConfirmEmailAsync(user, request.Token);

                if (confirmEmailResult.Succeeded)
                {
                    return Unit.Value;
                }

                throw new IdentityException("Problem confirming email", confirmEmailResult.Errors);
            }
        }
    }
}
