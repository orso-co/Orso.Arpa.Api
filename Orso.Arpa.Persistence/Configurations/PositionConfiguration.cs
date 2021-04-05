using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder
                .HasOne(e => e.Section)
                .WithMany(v => v.Positions)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.Name)
                .HasMaxLength(50);
        }
    }
}
