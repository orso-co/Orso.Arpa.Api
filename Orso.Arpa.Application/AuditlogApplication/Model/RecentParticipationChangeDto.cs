using System;
using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Application.AuditLogApplication.Model
{
    /// <summary>
    /// Represents a recent participation change (project or appointment) with resolved names.
    /// Used for the Dashboard activity feed.
    /// </summary>
    public class RecentParticipationChangeDto
    {
        /// <summary>
        /// The ID of the audit log entry
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// When the change occurred
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Type of participation change: "Project" or "Appointment"
        /// </summary>
        public string ParticipationType { get; set; }

        /// <summary>
        /// The name of the person who changed their participation
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// The ID of the person
        /// </summary>
        public Guid? PersonId { get; set; }

        /// <summary>
        /// The name of the project (for project participations) or the appointment details (for appointment participations)
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// The ID of the project or appointment
        /// </summary>
        public Guid? TargetId { get; set; }

        /// <summary>
        /// The participation status result (Acceptance, Refusal, Pending)
        /// </summary>
        public string StatusResult { get; set; }

        /// <summary>
        /// The inner participation status
        /// </summary>
        public string StatusInner { get; set; }
    }
}
