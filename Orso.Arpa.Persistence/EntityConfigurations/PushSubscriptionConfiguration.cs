using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PushSubscriptionConfiguration : IEntityTypeConfiguration<PushSubscription>
    {
        public void Configure(EntityTypeBuilder<PushSubscription> builder)
        {
            builder
                .HasIndex(e => e.Endpoint)
                .IsUnique();

            builder
                .HasIndex(e => e.UserId);

            builder
                .Property(e => e.Endpoint)
                .HasMaxLength(2048)
                .IsRequired();

            builder
                .Property(e => e.P256dh)
                .HasMaxLength(512)
                .IsRequired();

            builder
                .Property(e => e.Auth)
                .HasMaxLength(512)
                .IsRequired();

            builder
                .Property(e => e.UserAgent)
                .HasMaxLength(500);

            builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
