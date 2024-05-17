using System;

namespace Orso.Arpa.Domain._General.Errors
{
    public class SystemStartException : Exception
    {
        public SystemStartException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
