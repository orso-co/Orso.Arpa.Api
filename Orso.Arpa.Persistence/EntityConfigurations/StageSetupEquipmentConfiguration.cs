using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class StageSetupEquipmentConfiguration : IEntityTypeConfiguration<StageSetupEquipment>
    {
        public void Configure(EntityTypeBuilder<StageSetupEquipment> builder)
        {
            builder.ToTable("stage_setup_equipment");

            // Equipment type identifier
            builder
                .Property(e => e.EquipmentId)
                .HasMaxLength(100)
                .IsRequired();

            // Position coordinates (percentage 0-100)
            builder
                .Property(e => e.PositionX)
                .IsRequired();

            builder
                .Property(e => e.PositionY)
                .IsRequired();

            // Rotation in degrees
            builder
                .Property(e => e.Rotation)
                .IsRequired();

            // StageSetup relationship
            builder
                .HasOne(e => e.StageSetup)
                .WithMany(s => s.Equipment)
                .HasForeignKey(e => e.StageSetupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index for faster queries by setup
            builder
                .HasIndex(e => e.StageSetupId);
        }
    }
}
