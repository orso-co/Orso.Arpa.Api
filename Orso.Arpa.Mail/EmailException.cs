using System;

namespace Orso.Arpa.Mail
{
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
    }
}
