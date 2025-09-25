using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Model
{
    public class UrlRole : BaseEntity
    {
        public UrlRole(Guid? id, Url url, Role role) : base(id ?? Guid.NewGuid())
        {
            Url = url;
            Role = role;
        }

        public UrlRole(Guid urlId, Guid roleId)
        {
            UrlId = urlId;
            RoleId = roleId;
        }

        public UrlRole()
        {
        }

        public Guid UrlId { get; set; }
        public virtual Url Url { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
