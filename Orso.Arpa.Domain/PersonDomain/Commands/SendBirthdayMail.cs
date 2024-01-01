

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.PersonDomain.Commands;

public static class SendBirthdayMail {

    public class Command : IRequest
    {
        public string RecipientEMailAddress { get; set; }
        public string RecipientName { get; set; }
    }

    public class Handler : IRequestHandler<Command> {

            private readonly JwtConfiguration _jwtConfiguration;
            private readonly ClubConfiguration _clubConfiguration;
            private readonly IEmailSender _emailSender;

            public Handler(JwtConfiguration jwtConfiguration,
                           ClubConfiguration clubConfiguration,
                           IEmailSender emailSender)
            {
                _jwtConfiguration = jwtConfiguration;
                _clubConfiguration = clubConfiguration;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {
                var template = new BirthdayTemplate
                    {
                        DisplayName = request.RecipientName,
                        ArpaLogo = _jwtConfiguration.ArpaLogo,
                        ClubAddress = _clubConfiguration.Address,
                        ClubMail = _clubConfiguration.ContactEmail,
                        ClubName = _clubConfiguration.Name,
                        ClubPhoneNumber = _clubConfiguration.Phone
                    };
                    await _emailSender.SendTemplatedEmailAsync(template, request.RecipientEMailAddress);
                    return Unit.Value;
            }
    }
}