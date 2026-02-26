using System;
using Orso.Arpa.Domain.General.Attributes;

namespace Orso.Arpa.Domain.ActivityLogDomain.Model;

[AuditLogIgnore]
public class ActivityLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Action { get; set; }
    public string Category { get; set; }
    public string EntityType { get; set; }
    public Guid? EntityId { get; set; }
    public string EntityLabel { get; set; }
    public string Path { get; set; }
    public string Metadata { get; set; }
}
