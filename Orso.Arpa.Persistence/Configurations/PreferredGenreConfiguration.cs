using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class PreferredGenreConfiguration : IEntityTypeConfiguration<PreferredGenre>
    {
        public void Configure(EntityTypeBuilder<PreferredGenre> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.SelectValueMappingId });

            builder
                .HasOne(e => e.SelectValueMapping)
                .WithMany(r => r.PreferredGenres)
                .HasForeignKey(e => e.SelectValueMappingId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.PreferredGenres)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
