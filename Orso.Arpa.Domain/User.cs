using System;
using Microsoft.AspNetCore.Identity;

namespace Orso.Arpa.Domain
{
    public class User : IdentityUser<Guid>
    {
        public bool Deleted { get; private set; }

        public void Delete()
        {
            Deleted = true;
        }
    }
}
