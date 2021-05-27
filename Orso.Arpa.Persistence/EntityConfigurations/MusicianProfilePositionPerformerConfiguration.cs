using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfilePositionPerformerConfiguration : IEntityTypeConfiguration<MusicianProfilePositionPerformer>
    {
        public void Configure(EntityTypeBuilder<MusicianProfilePositionPerformer> builder)
        {
            builder
                .HasOne(e => e.SelectValueSection)
                .WithMany(r => r.MusicianProfilePositionsAsPerformer)
                .HasForeignKey(e => e.SelectValueSectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.PreferredPositionsPerformer)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
