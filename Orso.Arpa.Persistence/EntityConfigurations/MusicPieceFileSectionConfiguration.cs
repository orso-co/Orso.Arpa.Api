using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceFileSectionConfiguration : IEntityTypeConfiguration<MusicPieceFileSection>
    {
        public void Configure(EntityTypeBuilder<MusicPieceFileSection> builder)
        {
            _ = builder
                .HasOne(e => e.MusicPieceFile)
                .WithMany(f => f.Sections)
                .HasForeignKey(e => e.MusicPieceFileId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = builder
                .HasOne(e => e.Section)
                .WithMany()
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = builder.HasIndex(e => new { e.MusicPieceFileId, e.SectionId }).IsUnique();
        }
    }
}
