using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class User : IdentityUser<Guid>
    {
        public void Update(ModifyMyUser.Command command)
        {
            Email = command.Email;
            Person.Update(command);
        }

        public string DisplayName => Person.DisplayName;
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
