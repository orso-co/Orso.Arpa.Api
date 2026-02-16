using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class InstrumentationConfiguration : IEntityTypeConfiguration<Instrumentation>
    {
        public void Configure(EntityTypeBuilder<Instrumentation> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(2000);

            builder.Property(e => e.IsTemplate)
                .HasDefaultValue(false);

            builder.HasIndex(e => e.IsTemplate);
            builder.HasIndex(e => e.ProjectId);
            builder.HasIndex(e => new { e.IsTemplate, e.ProjectId });

            builder.HasOne(e => e.Project)
                .WithMany(p => p.Instrumentations)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Positions)
                .WithOne(p => p.Instrumentation)
                .HasForeignKey(p => p.InstrumentationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
