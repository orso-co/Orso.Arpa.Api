using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPiecePartConfiguration : IEntityTypeConfiguration<MusicPiecePart>
    {
        public void Configure(EntityTypeBuilder<MusicPiecePart> builder)
        {
            _ = builder
                .Property(e => e.PartName)
                .HasMaxLength(200)
                .IsRequired();

            _ = builder
                .HasOne(e => e.MusicPiece)
                .WithMany(p => p.Parts)
                .HasForeignKey(e => e.MusicPieceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = builder
                .HasOne(e => e.Section)
                .WithMany()
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder.HasIndex(e => new { e.MusicPieceId, e.SortOrder });
        }
    }
}
