using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfilePositionInnerConfiguration : IEntityTypeConfiguration<MusicianProfilePositionInner>
    {
        public void Configure(EntityTypeBuilder<MusicianProfilePositionInner> builder)
        {
            builder
                .HasOne(e => e.SelectValueSection)
                .WithMany(r => r.MusicianProfilePositionsAsInner)
                .HasForeignKey(e => e.SelectValueSectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.PreferredPositionsInner)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
