using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Model
{
    [AuditLogIgnore]
    public class CalendarFeedToken : BaseEntity
    {
        public CalendarFeedToken(Guid? id, Guid userId, string token, string feedType, Guid? projectId = null)
            : base(id)
        {
            UserId = userId;
            Token = token;
            FeedType = feedType;
            ProjectId = projectId;
            IsActive = true;
        }

        [JsonConstructor]
        protected CalendarFeedToken()
        {
        }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        [MaxLength(64)]
        public string Token { get; private set; }

        [MaxLength(20)]
        public string FeedType { get; private set; }

        public Guid? ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        public DateTime? LastAccessedAt { get; private set; }
        public bool IsActive { get; private set; }

        public void UpdateLastAccessed(DateTime utcNow)
        {
            LastAccessedAt = utcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
