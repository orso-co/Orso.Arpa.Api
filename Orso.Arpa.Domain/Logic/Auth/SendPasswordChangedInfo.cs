using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public static class SendPasswordChangedInfo
    {
        public class Command : IRequest
        {
            public string UsernameOrEmail { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager)
            {
                RuleFor(c => c.UsernameOrEmail)
                    .MustAsync(async (username, cancellation) => await userManager.FindUserByUsernameOrEmailAsync(username) != null)
                    .WithErrorCode("404")
                    .WithMessage("User could not be found.")
                    .When(cmd => !string.IsNullOrEmpty(cmd.UsernameOrEmail));
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly ClubConfiguration _clubConfiguration;
            private readonly IEmailSender _emailSender;
            private readonly IUserAccessor _userAccessor;

            public Handler(ArpaUserManager userManager,
                           JwtConfiguration jwtConfiguration,
                           ClubConfiguration clubConfiguration,
                           IEmailSender emailSender,
                           IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _jwtConfiguration = jwtConfiguration;
                _clubConfiguration = clubConfiguration;
                _emailSender = emailSender;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user;
                if (string.IsNullOrEmpty(request.UsernameOrEmail))
                {
                    user = await _userAccessor.GetCurrentUserAsync();
                }
                else
                {
                    user = await _userManager.FindUserByUsernameOrEmailAsync(request.UsernameOrEmail);
                }

                var template = new PasswordChangedTemplate
                {
                    DisplayName = user.DisplayName,
                    ArpaLogo = $"{_jwtConfiguration.Audience}/images/arpa_logo.png",
                    ClubAddress = _clubConfiguration.Address,
                    ClubMail = _clubConfiguration.ContactEmail,
                    ClubName = _clubConfiguration.Name,
                    ClubPhoneNumber = _clubConfiguration.Phone
                };

                await _emailSender.SendTemplatedEmailAsync(template, user.Email);

                return Unit.Value;
            }
        }
    }
}
