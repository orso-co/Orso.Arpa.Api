using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class AppointmentRoomConfiguration : IEntityTypeConfiguration<AppointmentRoom>
    {
        public void Configure(EntityTypeBuilder<AppointmentRoom> builder)
        {
            builder.HasKey(e => new { e.AppointmentId, e.RoomId });
        }
    }
}
