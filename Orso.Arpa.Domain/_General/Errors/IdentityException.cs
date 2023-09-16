using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.General.Errors
{
    [Serializable]
    public class IdentityException : Exception
    {
        private readonly IEnumerable<IdentityError> _identityErrors;
        
        public IdentityException(string message, IEnumerable<IdentityError> identityErrors) : base(message)
        {
            _identityErrors = identityErrors ?? new List<IdentityError>();
        }

        public IEnumerable<IdentityError> IdentityErrors { get { return _identityErrors; } }

        public IdentityException()
        {
        }

        public IdentityException(string message) : base(message)
        {
        }

        public IdentityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
