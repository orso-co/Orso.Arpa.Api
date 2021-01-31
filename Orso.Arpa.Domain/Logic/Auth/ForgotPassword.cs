using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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
            private readonly ClubConfiguration _clubConfiguration;
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly IEmailSender _emailSender;

            public Handler(
                UserManager<User> userManager,
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
                User user = await _userManager.FindByNameAsync(request.UserName);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var param = new Dictionary<string, string>
                        {
                            { "token", token },
                            { "username", request.UserName }
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
