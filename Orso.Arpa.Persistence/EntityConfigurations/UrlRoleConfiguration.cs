using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class UrlRoleConfiguration : IEntityTypeConfiguration<UrlRole>
    {
        public void Configure(EntityTypeBuilder<UrlRole> builder)
        {
            builder
                .HasOne(e => e.Role)
                .WithMany(r => r.UrlRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Url)
                .WithMany(r => r.UrlRoles)
                .HasForeignKey(e => e.UrlId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
