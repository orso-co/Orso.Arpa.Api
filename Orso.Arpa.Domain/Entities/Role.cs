using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public short Level { get; set; }
        public virtual ICollection<UrlRole> UrlRoles { get; private set; } = new HashSet<UrlRole>();
    }
}
