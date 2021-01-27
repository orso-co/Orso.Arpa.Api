using System;

namespace Orso.Arpa.Domain.Errors
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}
