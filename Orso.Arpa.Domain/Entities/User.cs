using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public bool Deleted { get; private set; }
        public string DisplayName => $"{Person.GivenName} {Person.Surname}";
        public virtual Person Person { get; set; }
        public Guid PersonId { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; private set; } = new HashSet<RefreshToken>();

        public void Delete()
        {
            Deleted = true;
        }
    }
}
