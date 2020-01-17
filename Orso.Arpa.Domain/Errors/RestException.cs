using System;
using System.Net;

namespace Orso.Arpa.Domain.Errors
{
#pragma warning disable RCS1194 // Implement exception constructors.

    public class RestException : Exception
#pragma warning restore RCS1194 // Implement exception constructors.
    {
        public RestException(string message, HttpStatusCode code, object errors = null) : base(message)
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; }
        public object Errors { get; }
    }
}
