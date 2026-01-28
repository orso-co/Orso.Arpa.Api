using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class StageSetupConfiguration : IEntityTypeConfiguration<StageSetup>
    {
        public void Configure(EntityTypeBuilder<StageSetup> builder)
        {
            builder.ToTable("stage_setups");

            // Name field
            builder
                .Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            // File properties
            builder
                .Property(e => e.FileName)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .Property(e => e.StoragePath)
                .HasMaxLength(1000)
                .IsRequired();

            builder
                .Property(e => e.ContentType)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(e => e.FileSize)
                .IsRequired();

            // Canvas dimensions
            builder
                .Property(e => e.CanvasWidth)
                .IsRequired();

            builder
                .Property(e => e.CanvasHeight)
                .IsRequired();

            // Boolean flags
            builder
                .Property(e => e.IsActive)
                .HasDefaultValue(false);

            builder
                .Property(e => e.IsVisibleToPerformers)
                .HasDefaultValue(false);

            // Project relationship
            builder
                .HasOne(e => e.Project)
                .WithMany(p => p.StageSetups)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Positions collection - configured in StageSetupPositionConfiguration

            // Index for faster project queries
            builder
                .HasIndex(e => e.ProjectId);

            // Index for active setups per project
            builder
                .HasIndex(e => new { e.ProjectId, e.IsActive });
        }
    }
}
