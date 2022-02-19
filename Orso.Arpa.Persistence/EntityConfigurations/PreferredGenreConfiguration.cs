using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PreferredGenreConfiguration : IEntityTypeConfiguration<PreferredGenre>
    {
        public void Configure(EntityTypeBuilder<PreferredGenre> builder)
        {
            builder
                .HasOne(e => e.SelectValueMapping)
                .WithMany()
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
