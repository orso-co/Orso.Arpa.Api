using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class InstrumentationPositionDoublingConfiguration : IEntityTypeConfiguration<InstrumentationPositionDoubling>
    {
        public void Configure(EntityTypeBuilder<InstrumentationPositionDoubling> builder)
        {
            builder.HasOne(e => e.InstrumentationPosition)
                .WithMany(p => p.Doublings)
                .HasForeignKey(e => e.InstrumentationPositionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Section)
                .WithMany()
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.InstrumentationPositionId);
        }
    }
}
