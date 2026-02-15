using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class UserDashboardLayoutConfiguration : IEntityTypeConfiguration<UserDashboardLayout>
    {
        public void Configure(EntityTypeBuilder<UserDashboardLayout> builder)
        {
            builder
                .HasIndex(e => new { e.UserId, e.DashboardType })
                .IsUnique();

            builder
                .Property(e => e.DashboardType)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(e => e.LayoutData)
                .HasColumnType("text");

            builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
