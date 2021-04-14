using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Domain.Entities
{
    public class Audit
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public AuditType Type { get; set; }
        public string TableName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public IList<string> ChangedColumns { get; } = new List<string>();
        public Dictionary<string, Guid> KeyValues { get; } = new Dictionary<string, Guid>();

    }
}
