using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder
                .HasData(RegionSeedData.Regions);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);
        }
    }
}
