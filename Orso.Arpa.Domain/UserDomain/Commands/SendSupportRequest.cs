using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public static class SendSupportRequest
    {
        public class Command : IRequest
        {
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Message { get; set; }
            public List<string> Topics { get; set; } = new();
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.GivenName)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(100);

                RuleFor(c => c.Surname)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(100);

                RuleFor(c => c.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MaximumLength(256);

                RuleFor(c => c.Message)
                    .NotEmpty()
                    .MinimumLength(10)
                    .MaximumLength(5000);

                RuleFor(c => c.Topics)
                    .NotEmpty()
                    .WithMessage("At least one topic must be selected.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly JwtConfiguration _jwtConfiguration;
            private readonly ClubConfiguration _clubConfiguration;
            private readonly IEmailSender _emailSender;

            private static readonly Dictionary<string, string> TopicLabels = new()
            {
                { "loginFailed", "Login schlägt fehl" },
                { "passwordMailNotReceived", "Mail mit Link für neues Passwort kommt nicht an" },
                { "forgotUsername", "Benutzername/E-Mail-Adresse vergessen" },
                { "other", "Etwas anderes" }
            };

            public Handler(
                JwtConfiguration jwtConfiguration,
                ClubConfiguration clubConfiguration,
                IEmailSender emailSender)
            {
                _jwtConfiguration = jwtConfiguration;
                _clubConfiguration = clubConfiguration;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var topicsFormatted = string.Join(", ", request.Topics
                    .Select(t => TopicLabels.TryGetValue(t, out var label) ? label : t));

                var template = new SupportRequestTemplate
                {
                    GivenName = request.GivenName,
                    Surname = request.Surname,
                    Email = request.Email,
                    Username = string.IsNullOrEmpty(request.Username) ? "(nicht angegeben)" : request.Username,
                    Message = request.Message,
                    TopicsFormatted = topicsFormatted,
                    ArpaLogo = _jwtConfiguration.ArpaLogo,
                    ClubAddress = _clubConfiguration.Address,
                    ClubMail = _clubConfiguration.ContactEmail,
                    ClubName = _clubConfiguration.Name,
                    ClubPhoneNumber = _clubConfiguration.Phone
                };

                var supportEmail = !string.IsNullOrEmpty(_clubConfiguration.SupportEmail)
                    ? _clubConfiguration.SupportEmail
                    : _clubConfiguration.ContactEmail;

                await _emailSender.SendTemplatedEmailAsync(template, supportEmail);

                return Unit.Value;
            }
        }
    }
}
