using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class UrlRoleConfiguration : IEntityTypeConfiguration<UrlRole>
    {
        public void Configure(EntityTypeBuilder<UrlRole> builder)
        {
            builder.HasKey(e => new { e.UrlId, e.RoleId });

            builder
                .HasOne(e => e.Role)
                .WithMany(r => r.UrlRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder
                .HasOne(e => e.Url)
                .WithMany(r => r.UrlRoles)
                .HasForeignKey(e => e.UrlId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
