using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AppointmentSetlistPieceConfiguration : IEntityTypeConfiguration<AppointmentSetlistPiece>
    {
        public void Configure(EntityTypeBuilder<AppointmentSetlistPiece> builder)
        {
            builder
                .HasOne(e => e.Appointment)
                .WithMany(a => a.PrioritizedPieces)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.SetlistPiece)
                .WithMany()
                .HasForeignKey(e => e.SetlistPieceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique combination of appointment + setlist piece
            builder
                .HasIndex(e => new { e.AppointmentId, e.SetlistPieceId })
                .IsUnique();
        }
    }
}
