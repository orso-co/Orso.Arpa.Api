using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AppointmentRoomConfiguration : IEntityTypeConfiguration<AppointmentRoom>
    {
        public void Configure(EntityTypeBuilder<AppointmentRoom> builder)
        {
            builder
                .HasOne(e => e.Room)
                .WithMany(r => r.AppointmentRooms)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Appointment)
                .WithMany(r => r.AppointmentRooms)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
