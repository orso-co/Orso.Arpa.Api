using System;

namespace Orso.Arpa.Domain.General.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HardDeleteAttribute : Attribute
    {
    }
}
