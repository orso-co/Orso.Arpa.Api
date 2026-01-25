using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceFileConfiguration : IEntityTypeConfiguration<MusicPieceFile>
    {
        public void Configure(EntityTypeBuilder<MusicPieceFile> builder)
        {
            _ = builder
                .Property(e => e.FileName)
                .HasMaxLength(500)
                .IsRequired();

            _ = builder
                .Property(e => e.StorageFileName)
                .HasMaxLength(500)
                .IsRequired();

            _ = builder
                .Property(e => e.ContentType)
                .HasMaxLength(100)
                .IsRequired();

            _ = builder
                .Property(e => e.Description)
                .HasMaxLength(500);

            _ = builder
                .HasOne(e => e.MusicPiece)
                .WithMany(p => p.Files)
                .HasForeignKey(e => e.MusicPieceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = builder
                .HasOne(e => e.MusicPiecePart)
                .WithMany(p => p.Files)
                .HasForeignKey(e => e.MusicPiecePartId)
                .OnDelete(DeleteBehavior.SetNull);

            _ = builder.HasIndex(e => e.MusicPieceId);
            _ = builder.HasIndex(e => e.MusicPiecePartId);
        }
    }
}
