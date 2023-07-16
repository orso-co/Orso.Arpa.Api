using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Misc;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Domain.Logic.Auth
{
    public class UserRegisteredNotification : INotification
    {
        public string UserName { get; set; }
    }

    public class SendUserRegisteredInfoToKbb : INotificationHandler<UserRegisteredNotification>
    {
        private readonly ArpaUserManager _userManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ILogger<SendUserRegisteredInfoToKbb> _logger;

        public SendUserRegisteredInfoToKbb(
            ArpaUserManager userManager,
            JwtConfiguration jwtConfiguration,
            ILogger<SendUserRegisteredInfoToKbb> logger)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration;
            _logger = logger;
        }

        public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(notification.UserName);

            KbbInfoLogger.LogInfoForKbb(
                _logger,
                "user registered",
                new Dictionary<string, object>
                {
                    { "Person", user.Person },
                    { "E-Mail", user.Email },
                    { "Link", $"{_jwtConfiguration.Audience}/arpa/dashboard/admin".FormatLink("Go to Admin Dashboard") }
                },
                "The user must now confirm their email address");
        }
    }
}

