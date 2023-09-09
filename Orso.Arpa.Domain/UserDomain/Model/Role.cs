using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class Role : IdentityRole<Guid>
    {
        public short Level { get; set; }
        public virtual ICollection<UrlRole> UrlRoles { get; private set; } = new HashSet<UrlRole>();

        [AuditLogIgnore]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
    }
}
