using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceFileAnnotationConfiguration : IEntityTypeConfiguration<MusicPieceFileAnnotation>
    {
        public void Configure(EntityTypeBuilder<MusicPieceFileAnnotation> builder)
        {
            _ = builder.ToTable("music_piece_file_annotations");

            _ = builder.HasKey(e => e.Id);

            _ = builder
                .Property(e => e.AnnotationData)
                .HasColumnType("jsonb");

            _ = builder
                .Property(e => e.ModifiedAt)
                .IsRequired();

            _ = builder
                .HasIndex(e => new { e.MusicPieceFileId, e.UserId })
                .IsUnique();

            _ = builder.HasIndex(e => e.UserId);
        }
    }
}
