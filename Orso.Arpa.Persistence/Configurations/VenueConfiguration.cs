using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder
                .HasOne(e => e.Address)
                .WithOne(a => a.Venue)
                .HasForeignKey<Venue>(e => e.AddressId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
