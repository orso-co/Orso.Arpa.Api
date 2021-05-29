using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileSectionConfiguration : IEntityTypeConfiguration<MusicianProfileSection>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileSection> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.SectionId });

            builder
                .HasOne(e => e.Section)
                .WithMany(r => r.MusicianProfileSections)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.DoublingInstruments)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(a => a.InstrumentAvailability)
                .WithMany()
                .HasForeignKey(a => a.InstrumentAvailabilityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
