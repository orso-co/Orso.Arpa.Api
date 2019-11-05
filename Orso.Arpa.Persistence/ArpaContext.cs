using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Persistence
{
    public class ArpaContext : IdentityDbContext<User, AppRole, Guid>
    {
        public ArpaContext(DbContextOptions options) : base(options)
        {

        }
    }
}
