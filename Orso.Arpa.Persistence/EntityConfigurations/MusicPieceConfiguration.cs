using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceConfiguration : IEntityTypeConfiguration<MusicPiece>
    {
        public void Configure(EntityTypeBuilder<MusicPiece> builder)
        {
            _ = builder
                .Property(e => e.Title)
                .HasMaxLength(500)
                .IsRequired();

            _ = builder
                .Property(e => e.Composer)
                .HasMaxLength(200);

            _ = builder
                .Property(e => e.Arranger)
                .HasMaxLength(200);

            _ = builder
                .Property(e => e.Subtitle)
                .HasMaxLength(500);

            _ = builder
                .Property(e => e.Opus)
                .HasMaxLength(100);

            _ = builder
                .Property(e => e.Instrumentation)
                .HasMaxLength(2000);

            _ = builder
                .Property(e => e.PerformanceNotes)
                .HasMaxLength(5000);

            _ = builder
                .Property(e => e.InternalNotes)
                .HasMaxLength(5000);

            // Indexes for common search fields
            _ = builder.HasIndex(e => e.Title);
            _ = builder.HasIndex(e => e.Composer);
            _ = builder.HasIndex(e => e.IsArchived);

            // SelectValue relationships
            _ = builder
                .HasOne(e => e.Epoch)
                .WithMany()
                .HasForeignKey(e => e.EpochId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Genre)
                .WithMany()
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.DifficultyLevel)
                .WithMany()
                .HasForeignKey(e => e.DifficultyLevelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
