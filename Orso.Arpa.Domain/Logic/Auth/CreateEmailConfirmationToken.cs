using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

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
            private readonly ClubConfiguration _clubConfiguration;
            private readonly JwtConfiguration _jwtConfiguration;

            public Handler(
                UserManager<User> userManager,
                ClubConfiguration clubConfiguration,
                JwtConfiguration jwtConfiguration,
                IEmailSender emailSender)
            {
                _emailSender = emailSender;
                _userManager = userManager;
                _clubConfiguration = clubConfiguration;
                _jwtConfiguration = jwtConfiguration;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByEmailAsync(request.Email);

                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.Email), "The email address is already confirmed") });
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var param = new Dictionary<string, string>
                        {
                            { "token", token },
                            { "email", request.Email }
                        };
                var uri = QueryHelpers.AddQueryString(request.ClientUri, param);

                var template = new ConfirmEmailTemplate
                {
                    DisplayName = user.DisplayName,
                    ArpaLogo = $"{_jwtConfiguration.Audience}/images/arpa_logo.png",
                    ClientUri = uri,
                    ClubAddress = _clubConfiguration.Address,
                    ClubMail = _clubConfiguration.Email,
                    ClubName = _clubConfiguration.Name,
                    ClubPhoneNumber = _clubConfiguration.Phone
                };

                await _emailSender.SendTemplatedEmailAsync(template, request.Email);

                return Unit.Value;
            }
        }
    }
}
