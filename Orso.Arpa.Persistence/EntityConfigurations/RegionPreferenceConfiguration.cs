using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RegionPreferenceConfiguration : IEntityTypeConfiguration<RegionPreference>
    {
        public void Configure(EntityTypeBuilder<RegionPreference> builder)
        {
            builder
                .HasOne(e => e.Region)
                .WithMany(r => r.RegionPreferences)
                .HasForeignKey(e => e.RegionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.RegionPreferences)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .Property(e => e.Comment)
                .HasMaxLength(500);
        }
    }
}
