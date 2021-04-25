using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class RegionPreferenceRehearsalConfiguration : IEntityTypeConfiguration<RegionPreferenceRehearsal>
    {
        public void Configure(EntityTypeBuilder<RegionPreferenceRehearsal> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.VenueId });

            builder
                .HasOne(e => e.Venue)
                .WithMany(r => r.RegionPreferenceRehearsals)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.RegionPreferenceRehearsals)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
