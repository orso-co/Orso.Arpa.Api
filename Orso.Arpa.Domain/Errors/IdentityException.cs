using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.Errors
{
#pragma warning disable RCS1194 // Implement exception constructors.

    public class IdentityException : Exception
#pragma warning restore RCS1194 // Implement exception constructors.
    {
        public IdentityException(string message, IEnumerable<IdentityError> identityErrors) : base(message)
        {
            IdentityErrors = identityErrors ?? new List<IdentityError>();
        }

        public IEnumerable<IdentityError> IdentityErrors { get; }
    }
}
