using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ChangeLog;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Domain.Entities
{
    [AuditLogIgnore]
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuditLogType Type { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public IList<string> ChangedColumns { get; } = new List<string>();
        public string KeyValues { get; set; } = "{}";
    }
}
