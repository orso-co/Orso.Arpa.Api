using System;
using System.Net;

namespace Orso.Arpa.Domain.Errors
{
    public class RestException : Exception
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
