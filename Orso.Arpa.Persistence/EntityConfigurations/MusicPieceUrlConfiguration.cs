using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicPieceUrlConfiguration : IEntityTypeConfiguration<MusicPieceUrl>
    {
        public void Configure(EntityTypeBuilder<MusicPieceUrl> builder)
        {
            _ = builder
                .Property(e => e.Href)
                .HasMaxLength(1000)
                .IsRequired();

            _ = builder
                .Property(e => e.AnchorText)
                .HasMaxLength(200);

            _ = builder
                .HasOne(e => e.MusicPiece)
                .WithMany(p => p.Urls)
                .HasForeignKey(e => e.MusicPieceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            _ = builder
                .HasOne(e => e.UrlType)
                .WithMany()
                .HasForeignKey(e => e.UrlTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder.HasIndex(e => e.MusicPieceId);
        }
    }
}
