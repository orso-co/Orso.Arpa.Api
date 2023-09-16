using System;


namespace Orso.Arpa.Domain.General.Errors
{
    public class NotFoundException : Exception
    {
        private readonly string _typeName;
        private readonly string _propertyName;
        
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string typeName, string propertyName)
            : base($"{typeName} could not be found.")
        {
            _typeName = typeName;
            _propertyName = propertyName;
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string TypeName { get { return _typeName; } }
        public string PropertyName { get { return _propertyName; } }
    }
}
