using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ActivityLogDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.Username)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.Action)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Category)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.EntityType)
            .HasMaxLength(100);

        builder.Property(e => e.EntityLabel)
            .HasMaxLength(500);

        builder.Property(e => e.Path)
            .HasMaxLength(2000);

        builder.Property(e => e.Metadata)
            .HasColumnType("jsonb");

        builder.HasIndex(e => e.CreatedAt);
        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => new { e.Action, e.Category });
    }
}
