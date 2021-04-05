using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder
               .HasData(SectionSeedData.Sections);

            builder
                .HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);
        }
    }
}
