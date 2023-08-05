using System;
using System.Runtime.Serialization;


namespace Orso.Arpa.Domain.Errors
{
    [Serializable]
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }

        public AuthorizationException() : base()
        {
        }

        public AuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
