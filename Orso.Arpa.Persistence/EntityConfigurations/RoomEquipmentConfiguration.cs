using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RoomEquipmentConfiguration : IEntityTypeConfiguration<RoomEquipment>
    {
        public void Configure(EntityTypeBuilder<RoomEquipment> builder)
        {
            builder
                .HasOne(e => e.Equipment)
                .WithMany()
                .HasForeignKey(e => e.EquipmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Room)
                .WithMany(a => a.RoomEquipments)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .Property(e => e.Description)
                .HasMaxLength(500);
        }
    }
}
