using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class SphereOfActivityConcertConfiguration : IEntityTypeConfiguration<SphereOfActivityConcert>
    {
        public void Configure(EntityTypeBuilder<SphereOfActivityConcert> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.VenueId });

            builder
                .HasOne(e => e.Venue)
                .WithMany(r => r.SphereOfActivityConcerts)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.SphereOfActivityConcerts)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
