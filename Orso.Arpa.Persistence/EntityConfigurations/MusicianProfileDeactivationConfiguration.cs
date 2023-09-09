using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileDeactivationConfiguration : IEntityTypeConfiguration<MusicianProfileDeactivation>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileDeactivation> builder)
        {
            builder
                .HasOne(e => e.MusicianProfile)
                .WithOne(m => m.Deactivation)
                .HasForeignKey<MusicianProfileDeactivation>(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .Property(e => e.Purpose)
                .HasMaxLength(500);
        }
    }
}
