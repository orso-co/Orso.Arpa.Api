using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfilePositionStaffConfiguration : IEntityTypeConfiguration<MusicianProfilePositionStaff>
    {
        public void Configure(EntityTypeBuilder<MusicianProfilePositionStaff> builder)
        {
            builder
                .HasOne(e => e.SelectValueSection)
                .WithMany(r => r.MusicianProfilePositionsAsStaff)
                .HasForeignKey(e => e.SelectValueSectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.PreferredPositionsStaff)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
