using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Misc;
using Orso.Arpa.Misc.Extensions;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public class EmailConfirmedNotification : INotification
    {
        public string UserName { get; set; }
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
            User user = await _userManager.FindByNameAsync(notification.UserName);

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

