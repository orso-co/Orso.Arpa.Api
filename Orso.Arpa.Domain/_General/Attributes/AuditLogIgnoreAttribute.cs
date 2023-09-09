using System;

namespace Orso.Arpa.Domain.General.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class AuditLogIgnoreAttribute : Attribute
    {
    }
}
