using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Orso.Arpa.Domain.AuditLogDomain.Enums;
using Orso.Arpa.Domain.General.Attributes;

namespace Orso.Arpa.Domain.AuditLogDomain.Model
{
    [AuditLogIgnore]
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuditLogType Type { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> OldValues { get; } = [];
        public Dictionary<string, object> NewValues { get; } = [];
        public IList<string> ChangedColumns { get; } = [];
        public string KeyValues { get; set; } = "{}";

        private static readonly JsonSerializerOptions s_serializerOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public string OldValuesJson => JsonSerializer.Serialize(OldValues, s_serializerOptions);

        public string NewValuesJson => JsonSerializer.Serialize(NewValues, s_serializerOptions);
    }
}
