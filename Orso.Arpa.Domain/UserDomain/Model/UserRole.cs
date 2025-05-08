using System;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; private set; }

        public virtual Role Role { get; private set; }
    }
}
