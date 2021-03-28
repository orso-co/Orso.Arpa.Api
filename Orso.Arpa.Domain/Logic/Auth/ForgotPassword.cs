using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class ForgotPassword
    {
        public class Command : IRequest
        {
            public string UsernameOrEmail { get; set; }
            public string ClientUri { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager)
            {
                RuleFor(c => c.UsernameOrEmail)
                    .MustAsync(async (username, cancellation) => await userManager.FindUserByUsernameOrEmailAsync(username) != null)
                    .OnFailure((request) => throw new NotFoundException(typeof(User).Name, nameof(Command.UsernameOrEmail)));
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly ClubConfiguration _clubConfiguration;
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly IEmailSender _emailSender;

            public Handler(
                ArpaUserManager userManager,
                ClubConfiguration clubConfiguration,
                JwtConfiguration jwtConfiguration,
                IEmailSender emailSender)
            {
                _userManager = userManager;
                _clubConfiguration = clubConfiguration;
                _jwtConfiguration = jwtConfiguration;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindUserByUsernameOrEmailAsync(request.UsernameOrEmail);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var param = new Dictionary<string, string>
                        {
                            { "token", token },
                            { "email", user.Email }
                        };
                var uri = QueryHelpers.AddQueryString(request.ClientUri, param);

                var template = new ResetPasswordTemplate
                {
                    DisplayName = user.DisplayName,
                    ArpaLogo = $"{_jwtConfiguration.Audience}/images/arpa_logo.png",
                    ClientUri = uri,
                    ClubAddress = _clubConfiguration.Address,
                    ClubMail = _clubConfiguration.Email,
                    ClubName = _clubConfiguration.Name,
                    ClubPhoneNumber = _clubConfiguration.Phone
                };

                await _emailSender.SendTemplatedEmailAsync(template, user.Email);

                return Unit.Value;
            }
        }
    }
}