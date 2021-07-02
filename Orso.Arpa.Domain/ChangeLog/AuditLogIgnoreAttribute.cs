using System;

namespace Orso.Arpa.Domain.ChangeLog
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class AuditLogIgnoreAttribute : Attribute
    {
    }
}
