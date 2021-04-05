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

            builder
                .HasOne(e => e.SelectValue)
                .WithMany(s => s.SelectValueMappings)
                .HasForeignKey(e => e.SelectValueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.SelectValueCategory)
                .WithMany(s => s.SelectValueMappings)
                .HasForeignKey(e => e.SelectValueCategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
