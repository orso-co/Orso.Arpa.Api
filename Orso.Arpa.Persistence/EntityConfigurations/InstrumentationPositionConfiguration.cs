using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class InstrumentationPositionConfiguration : IEntityTypeConfiguration<InstrumentationPosition>
    {
        public void Configure(EntityTypeBuilder<InstrumentationPosition> builder)
        {
            builder.Property(e => e.Label)
                .HasMaxLength(200);

            builder.Property(e => e.Comment)
                .HasMaxLength(1000);

            builder.Property(e => e.Quantity)
                .HasDefaultValue(1);

            builder.HasOne(e => e.Instrumentation)
                .WithMany(i => i.Positions)
                .HasForeignKey(e => e.InstrumentationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Section)
                .WithMany()
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Qualification)
                .WithMany()
                .HasForeignKey(e => e.QualificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Doublings)
                .WithOne(d => d.InstrumentationPosition)
                .HasForeignKey(d => d.InstrumentationPositionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.InstrumentationId);
        }
    }
}
