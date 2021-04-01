using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class MusicianProfileSelectValueMappingConfiguration : IEntityTypeConfiguration<MusicianProfileSelectValueMapping>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileSelectValueMapping> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.SelectValueMappingId });

            builder
                .HasOne(e => e.SelectValueMapping)
                .WithMany(r => r.MusicianProfileSelectValueMappingsAsPreferredGenres)
                .HasForeignKey(e => e.SelectValueMappingId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.PreferredGenres)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
