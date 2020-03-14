using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .HasOne(a => a.Region)
                .WithMany(r => r.Addresses)
                .HasForeignKey(a => a.RegionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(a => a.Address1)
                .HasMaxLength(50);

            builder
                .Property(a => a.Address2)
                .HasMaxLength(50);

            builder
                .Property(a => a.Zip)
                .HasMaxLength(10);

            builder
                .Property(a => a.City)
                .HasMaxLength(50);

            builder
                .Property(a => a.Country)
                .HasMaxLength(50);

            builder
                .Property(a => a.State)
                .HasMaxLength(50);
        }
    }
}
