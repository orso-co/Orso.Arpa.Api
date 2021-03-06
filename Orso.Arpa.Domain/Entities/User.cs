using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.ChangeLog;

namespace Orso.Arpa.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string DisplayName => $"{Person.GivenName} {Person.Surname}";
        public virtual Person Person { get; set; }
        public Guid PersonId { get; set; }

        [JsonInclude]
        public virtual ICollection<RefreshToken> RefreshTokens { get; private set; } = new HashSet<RefreshToken>();

        [JsonInclude]
        public DateTime CreatedAt { get; set; }

        [AuditLogIgnore]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [AuditLogIgnore]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }

        [AuditLogIgnore]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
    }
}
