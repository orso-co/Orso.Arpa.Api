using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain;
using Orso.Arpa.Persistence.Configurations;

namespace Orso.Arpa.Persistence
{
    public class ArpaContext : IdentityDbContext<User, Role, Guid>
    {
        public ArpaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
