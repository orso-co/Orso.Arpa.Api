using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RoomSectionConfiguration : IEntityTypeConfiguration<RoomSection>
    {
        public void Configure(EntityTypeBuilder<RoomSection> builder)
        {
            builder
                .HasOne(e => e.Section)
                .WithMany(s => s.RoomSections)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Room)
                .WithMany(a => a.RoomSections)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .Property(e => e.Description)
                .HasMaxLength(500);
        }
    }
}
