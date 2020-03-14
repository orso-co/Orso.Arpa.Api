using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.Configurations
{
    public class SelectValueConfiguration : IEntityTypeConfiguration<SelectValue>
    {
        public void Configure(EntityTypeBuilder<SelectValue> builder)
        {
            builder
                .HasData(SelectValueSeedData.SelectValues);

            builder
                .Property(s => s.Name)
                .HasMaxLength(50);

            builder
                .Property(s => s.Description)
                .HasMaxLength(255);
        }
    }
}
