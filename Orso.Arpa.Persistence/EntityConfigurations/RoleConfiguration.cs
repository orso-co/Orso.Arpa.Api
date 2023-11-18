using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {   
            builder
                .HasMany(e => e.UserRoles)
                .WithOne(p => p.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
