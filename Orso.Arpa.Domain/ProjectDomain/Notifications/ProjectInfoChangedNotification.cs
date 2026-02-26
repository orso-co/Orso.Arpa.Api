using System;
using MediatR;

namespace Orso.Arpa.Domain.ProjectDomain.Notifications
{
    public class ProjectInfoChangedNotification : INotification
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
