using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

        protected IdentityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _identityErrors = (IEnumerable<IdentityError>)info.GetValue(nameof(IdentityErrors), typeof(IList<string>)) 
                ?? new List<IdentityError>();
        }

        public IdentityException()
        {
        }

        public IdentityException(string message) : base(message)
        {
        }

        public IdentityException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(IdentityErrors), IdentityErrors, typeof(IEnumerable<IdentityError>));

            base.GetObjectData(info, context);
        }
    }
}
