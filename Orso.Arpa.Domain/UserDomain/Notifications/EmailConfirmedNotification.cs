using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Misc;
using Orso.Arpa.Misc.Extensions;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Domain.UserDomain.Notifications
{
    public class EmailConfirmedNotification : INotification
    {
        public string Email { get; set; }
    }

    public class SendUEmailConfirmedInfoToKbb : INotificationHandler<EmailConfirmedNotification>
    {
        private readonly ArpaUserManager _userManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILogger<SendUEmailConfirmedInfoToKbb> _logger;

        public SendUEmailConfirmedInfoToKbb(
            ArpaUserManager userManager,
            JwtConfiguration jwtConfiguration,
            ILogger<SendUEmailConfirmedInfoToKbb> logger)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration;
            _logger = logger;
        }

        public async Task Handle(EmailConfirmedNotification notification, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(notification.Email);

            KbbInfoLogger.LogInfoForKbb(
                _logger,
                "email confirmed",
                new Dictionary<string, object>
                {
                    { "Person", user.Person },
                    { "E-Mail", user.Email },
                    { "Registration Date", user.CreatedAt.ToGermanDateTimeString() },
                    { "Link", $"{_jwtConfiguration.Audience}/arpa/dashboard/admin".FormatLink("Go to Admin Dashboard") }
                },
                "The user is now waiting for their role assignment");
        }
    }
}

