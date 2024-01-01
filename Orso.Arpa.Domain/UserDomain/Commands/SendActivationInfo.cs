using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class SendActivationInfo
    {
        public class Command : IRequest
        {
            public string Username { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager)
            {
                RuleFor(c => c.Username)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                    .WithErrorCode("404")
                    .WithMessage("User could not be found.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly ClubConfiguration _clubConfiguration;
            private readonly IEmailSender _emailSender;

            public Handler(ArpaUserManager userManager,
                           JwtConfiguration jwtConfiguration,
                           ClubConfiguration clubConfiguration,
                           IEmailSender emailSender)
            {
                _userManager = userManager;
                _jwtConfiguration = jwtConfiguration;
                _clubConfiguration = clubConfiguration;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.Username);

                var template = new ActivationInfoTemplate
                {
                    DisplayName = user.DisplayName,
                    ArpaLogo = _jwtConfiguration.ArpaLogo,
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
