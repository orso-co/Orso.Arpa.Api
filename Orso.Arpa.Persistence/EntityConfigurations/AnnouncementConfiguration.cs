using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AnnouncementDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.Property(e => e.Title).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Content).HasMaxLength(2000);
        builder.Property(e => e.Priority).HasMaxLength(20).HasDefaultValue("info");
        builder.Property(e => e.Link).HasMaxLength(500);
        builder.Property(e => e.LinkText).HasMaxLength(100);
        builder.Property(e => e.SortOrder).HasDefaultValue(0);
    }
}

public class AnnouncementReadConfiguration : IEntityTypeConfiguration<AnnouncementRead>
{
    public void Configure(EntityTypeBuilder<AnnouncementRead> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => new { e.AnnouncementId, e.UserId }).IsUnique();

        builder.HasOne(e => e.Announcement)
            .WithMany(a => a.Reads)
            .HasForeignKey(e => e.AnnouncementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
