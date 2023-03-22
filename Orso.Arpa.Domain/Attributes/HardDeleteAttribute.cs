using System;

namespace Orso.Arpa.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HardDeleteAttribute : Attribute
    {
    }
}
