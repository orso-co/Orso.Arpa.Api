using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class NotificationPreferenceConfiguration : IEntityTypeConfiguration<NotificationPreference>
    {
        public void Configure(EntityTypeBuilder<NotificationPreference> builder)
        {
            builder
                .HasIndex(e => new { e.UserId, e.EventType })
                .IsUnique();

            builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
