using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class SetlistPieceConfiguration : IEntityTypeConfiguration<SetlistPiece>
    {
        public void Configure(EntityTypeBuilder<SetlistPiece> builder)
        {
            builder.Property(sp => sp.Notes)
                .HasMaxLength(1000);

            builder.HasIndex(sp => new { sp.SetlistId, sp.SortOrder });

            builder.HasOne(sp => sp.Setlist)
                .WithMany(s => s.Pieces)
                .HasForeignKey(sp => sp.SetlistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.MusicPiece)
                .WithMany()
                .HasForeignKey(sp => sp.MusicPieceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
