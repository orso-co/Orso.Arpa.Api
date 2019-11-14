using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.Errors
{
    public class IdentityException : Exception
    {
        public IdentityException(string message, IEnumerable<IdentityError> identityErrors) : base(message)
        {
            IdentityErrors = identityErrors ?? new List<IdentityError>();
        }

        public IEnumerable<IdentityError> IdentityErrors { get; }
    }
}
