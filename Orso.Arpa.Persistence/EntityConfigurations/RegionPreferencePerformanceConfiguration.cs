using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RegionPreferencePerformanceConfiguration : IEntityTypeConfiguration<RegionPreferencePerformance>
    {
        public void Configure(EntityTypeBuilder<RegionPreferencePerformance> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.VenueId });

            builder
                .HasOne(e => e.Venue)
                .WithMany(r => r.RegionPreferencePerformances)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.RegionPreferencePerformances)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
