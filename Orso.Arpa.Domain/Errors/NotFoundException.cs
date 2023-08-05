using System;
using System.Runtime.Serialization;


namespace Orso.Arpa.Domain.Errors
{

    [Serializable]
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


        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _propertyName = info.GetString(nameof(PropertyName));
            _typeName = info.GetString(nameof(TypeName));
        }

        public string TypeName { get { return _typeName; } }
        public string PropertyName { get { return _propertyName; } }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(TypeName), TypeName);
            info.AddValue(nameof(PropertyName), PropertyName);

            base.GetObjectData(info, context);
        }
    }
}
