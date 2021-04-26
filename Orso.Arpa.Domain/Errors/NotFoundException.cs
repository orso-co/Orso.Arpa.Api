using System;

namespace Orso.Arpa.Domain.Errors
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1194:Implement exception constructors.", Justification = "Only one specific constructor needed")]
    public class NotFoundException : Exception
    {
        public NotFoundException(string typeName, string propertyName)
            : base($"{typeName} could not be found.")
        {
            TypeName = typeName;
            PropertyName = propertyName;
        }

        public string TypeName { get; }
        public string PropertyName { get; }
    }
}
