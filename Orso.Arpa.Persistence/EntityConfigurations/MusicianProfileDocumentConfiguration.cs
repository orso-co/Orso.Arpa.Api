using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileDocumentConfiguration : IEntityTypeConfiguration<MusicianProfileDocument>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileDocument> builder)
        {
            builder
                .HasOne(e => e.SelectValueMapping)
                .WithMany()
                .HasForeignKey(e => e.SelectValueMappingId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.Documents)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
