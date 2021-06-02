using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfilePositionTeamConfiguration : IEntityTypeConfiguration<MusicianProfilePositionTeam>
    {
        public void Configure(EntityTypeBuilder<MusicianProfilePositionTeam> builder)
        {
            builder
                .HasOne(e => e.SelectValueSection)
                .WithMany(r => r.MusicianProfilePositionsAsTeam)
                .HasForeignKey(e => e.SelectValueSectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.PreferredPositionsTeam)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
