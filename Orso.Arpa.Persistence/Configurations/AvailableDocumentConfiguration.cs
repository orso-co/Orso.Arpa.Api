using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class AvailableDocumentConfiguration : IEntityTypeConfiguration<AvailableDocument>
    {
        public void Configure(EntityTypeBuilder<AvailableDocument> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.SelectValueMappingId });

            builder
                .HasOne(e => e.SelectValueMapping)
                .WithMany(r => r.AvailableDocumentStatus)
                .HasForeignKey(e => e.SelectValueMappingId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.AvailableDocumentStatus)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
