using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class SphereOfActivityRehearsalConfiguration : IEntityTypeConfiguration<SphereOfActivityRehearsal>
    {
        public void Configure(EntityTypeBuilder<SphereOfActivityRehearsal> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.VenueId });

            builder
                .HasOne(e => e.Venue)
                .WithMany(r => r.SphereOfActivityRehearsals)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.SphereOfActivityRehearsals)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
