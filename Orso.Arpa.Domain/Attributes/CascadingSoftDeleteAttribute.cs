using System;

namespace Orso.Arpa.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CascadingSoftDeleteAttribute : Attribute
    {
    }
}
