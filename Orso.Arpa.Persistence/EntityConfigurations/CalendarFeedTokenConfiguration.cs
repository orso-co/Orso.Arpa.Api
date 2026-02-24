using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class CalendarFeedTokenConfiguration : IEntityTypeConfiguration<CalendarFeedToken>
    {
        public void Configure(EntityTypeBuilder<CalendarFeedToken> builder)
        {
            builder.ToTable("calendar_feed_tokens");

            builder
                .HasIndex(e => e.Token)
                .IsUnique();

            builder
                .HasIndex(e => e.UserId);

            builder
                .Property(e => e.Token)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(e => e.FeedType)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
