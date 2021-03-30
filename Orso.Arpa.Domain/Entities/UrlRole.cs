using System;

namespace Orso.Arpa.Domain.Entities
{
    public class UrlRole : BaseEntity
    {
        public UrlRole(Guid? id, Url url, Role role) : base(id)
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
