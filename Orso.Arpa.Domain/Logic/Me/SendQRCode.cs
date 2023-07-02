using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Mail;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class SendQRCode
    {
        public class QrCodeFile
        {
            public byte[] Content { get; set; }
            public string FileName { get; set; }
            public static string ContentType => "image/png";
        }

        public class Command : IRequest<QrCodeFile>
        {
            public string Username { get; set; }
            public bool SendEmail { get; set; }
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

        public class Handler : IRequestHandler<Command, QrCodeFile>
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

            public async Task<QrCodeFile> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.Username);



                var qrCode = new QrCodeFile()
                {
                    Content = await ArpaQRCodeGenerator.GetQRCodeAsync(user.PersonId.ToString(), cancellationToken),
                    FileName = $"ARPA_QRCode_{user.DisplayName.Replace(' ', '_')}.png"
                };

                if (request.SendEmail)
                {
                    var template = new QRCodeTemplate
                    {
                        DisplayName = user.DisplayName,
                        ArpaLogo = $"{_jwtConfiguration.Audience}/images/arpa_logo.png",
                        ClubAddress = _clubConfiguration.Address,
                        ClubMail = _clubConfiguration.ContactEmail,
                        ClubName = _clubConfiguration.Name,
                        ClubPhoneNumber = _clubConfiguration.Phone
                    };

                    var attachments = new List<EmailAttachment>()
                {
                    new EmailAttachment { Content = qrCode.Content, FileName = qrCode.FileName }
                };

                    await _emailSender.SendTemplatedEmailAsync(template, user.Email, attachments);
                }

                return qrCode;
            }
        }
    }
}
