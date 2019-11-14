using System;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public bool Deleted { get; private set; }
        public string DisplayName { get; set; }
        public virtual Person Person { get; set; }
        public Guid PersonId { get; set; }

        public void Delete()
        {
            Deleted = true;
        }
    }
}
