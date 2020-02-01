using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.Configurations
{
    public class SelectValueMappingConfiguration : IEntityTypeConfiguration<SelectValueMapping>
    {
        public void Configure(EntityTypeBuilder<SelectValueMapping> builder)
        {
            builder
                .HasData(SelectValueMappingSeedData.SelectValueMappings);
        }
    }
}
