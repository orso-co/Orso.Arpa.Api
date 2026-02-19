using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Domain.ProjectDomain.Notifications
{
    public class SendProjectParticipationDashboardUpdate : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly IRealtimeNotificationSender _realtimeNotificationSender;
        private readonly ILogger<SendProjectParticipationDashboardUpdate> _logger;

        public SendProjectParticipationDashboardUpdate(
            IRealtimeNotificationSender realtimeNotificationSender,
            ILogger<SendProjectParticipationDashboardUpdate> logger)
        {
            _realtimeNotificationSender = realtimeNotificationSender;
            _logger = logger;
        }

        public async Task Handle(ProjectParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            var projectId = notification.ProjectParticipation?.Project?.Id;

            _logger.LogDebug("Sending dashboard update for project participation change, projectId: {ProjectId}", projectId);

            await _realtimeNotificationSender.SendDashboardUpdateToAllAsync("project", projectId);
            await _realtimeNotificationSender.SendDashboardUpdateToAllAsync("activity");
        }
    }
}
