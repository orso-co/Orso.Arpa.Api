using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; private set; }

        public virtual Role Role { get; private set; }
    }
}
