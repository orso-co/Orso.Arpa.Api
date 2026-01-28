using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class StageSetupPositionConfiguration : IEntityTypeConfiguration<StageSetupPosition>
    {
        public void Configure(EntityTypeBuilder<StageSetupPosition> builder)
        {
            builder.ToTable("stage_setup_positions");

            // Position coordinates (percentage 0-100)
            builder
                .Property(e => e.PositionX)
                .IsRequired();

            builder
                .Property(e => e.PositionY)
                .IsRequired();

            // Optional row and stand
            builder
                .Property(e => e.Row);

            builder
                .Property(e => e.Stand);

            // StageSetup relationship
            builder
                .HasOne(e => e.StageSetup)
                .WithMany(s => s.Positions)
                .HasForeignKey(e => e.StageSetupId)
                .OnDelete(DeleteBehavior.Cascade);

            // MusicianProfile relationship
            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany()
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique constraint: each musician can only be positioned once per setup
            // Uses a partial index to allow re-adding musicians whose positions were soft-deleted
            builder
                .HasIndex(e => new { e.StageSetupId, e.MusicianProfileId })
                .IsUnique()
                .HasFilter("deleted = false");

            // Index for faster queries by setup
            builder
                .HasIndex(e => e.StageSetupId);
        }
    }
}
