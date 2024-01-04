using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .HasOne(e => e.Venue)
                .WithMany(v => v.Rooms)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Capacity)
                .WithMany()
                .HasForeignKey(e => e.CapacityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .Property(e => e.Building)
                .HasMaxLength(50);

            builder
                .Property(e => e.Floor)
                .HasMaxLength(50);
        }
    }
}
