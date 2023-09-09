using System;

namespace Orso.Arpa.Domain.General.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CascadingSoftDeleteAttribute : Attribute
    {
    }
}
