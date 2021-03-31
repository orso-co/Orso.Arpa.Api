using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
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
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.DoublingInstruments)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
