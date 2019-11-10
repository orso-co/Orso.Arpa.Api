using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Orso.Arpa.Domain.Entities;
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
            SetDeletedQueryFilter(builder);

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        private static void SetDeletedQueryFilter(ModelBuilder builder)
        {
            foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
            {
                if (entity.ClrType.GetProperty(ArpaContextUtility.IsDeletedProperty) != null)
                {
                    builder.Entity(entity.ClrType)
                        .HasQueryFilter(ArpaContextUtility.GetIsDeletedRestriction(entity.ClrType));
                }
            }
        }
    }
}
