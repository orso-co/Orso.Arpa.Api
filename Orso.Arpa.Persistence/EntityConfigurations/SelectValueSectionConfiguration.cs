using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class SelectValueSectionConfiguration : IEntityTypeConfiguration<SelectValueSection>
    {
        public void Configure(EntityTypeBuilder<SelectValueSection> builder)
        {
            builder
                .HasOne(e => e.Section)
                .WithMany(s => s.SelectValueSections)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.SelectValue)
                .WithMany(s => s.InstrumentParts)
                .HasForeignKey(e => e.SelectValueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasData(SelectValueSectionSeedData.SelectValueSections);
        }
    }
}
