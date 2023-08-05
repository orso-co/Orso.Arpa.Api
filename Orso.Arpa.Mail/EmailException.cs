using System;
using System.Runtime.Serialization;


namespace Orso.Arpa.Mail
{
    [Serializable]
    public class EmailException : Exception
    {
        public EmailException() : base()
        {
        }

        public EmailException(string message) : base(message)
        {
        }

        public EmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
