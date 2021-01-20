using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Mail;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class ForgotPassword
    {
        public class Command : IRequest
        {
            public string UserName { get; set; }
            public string ClientUri { get; set; }
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
            private readonly IEmailSender _emailSender;

            public Handler(UserManager<User> userManager, IEmailSender emailSender)
            {
                _userManager = userManager;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.UserName);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // ToDo: E-Mail Message und Subject definieren. Frontend-Link einfügen

                var message = new EmailMessage(new string[] { user.Email }, "Reset password token", $"{request.ClientUri}{token}", false);
                await _emailSender.SendEmailAsync(message);

                return Unit.Value;
            }
        }
    }
}
