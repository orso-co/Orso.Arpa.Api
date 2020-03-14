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
        }
    }
}
