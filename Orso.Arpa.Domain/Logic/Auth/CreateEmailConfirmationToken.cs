using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Mail;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class CreateEmailConfirmationToken
    {
        public class Command : IRequest
        {
            public string Email { get; set; }
            public string ClientUri { get; set; }
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

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IEmailSender _emailSender;
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager,
                IEmailSender emailSender)
            {
                _emailSender = emailSender;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByEmailAsync(request.Email);

                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.Email), "The email address is already confirmed") });
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // ToDo: E-Mail Message und Subject definieren. Frontend-Link einfügen

                var param = new Dictionary<string, string>
                        {
                            { "token", token },
                            { "email", request.Email }
                        };
                var uri = QueryHelpers.AddQueryString(request.ClientUri, param);
                var message = new EmailMessage(new string[] { request.Email }, "Confirm email address", uri, false);
                await _emailSender.SendEmailAsync(message);

                return Unit.Value;
            }
        }
    }
}
