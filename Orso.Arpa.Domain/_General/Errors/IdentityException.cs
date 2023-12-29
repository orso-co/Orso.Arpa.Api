using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.General.Errors
{
    public class IdentityException : Exception
    {
        private readonly IEnumerable<IdentityError> _identityErrors = new List<IdentityError>();

        public IdentityException()
        {
        }

        public IdentityException(string message) : base(message)
        {
        }

        public IdentityException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public IdentityException(string message, IEnumerable<IdentityError> identityErrors) : base(message)
        {
            _identityErrors = identityErrors ?? new List<IdentityError>();
        }

        public IEnumerable<IdentityError> IdentityErrors { get { return _identityErrors; } }
    }
}
