using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .Property(e => e.Description)
                .HasMaxLength(255);

            builder
                .ComplexProperty(e => e.Address)
                .IsRequired();
        }
    }
}
