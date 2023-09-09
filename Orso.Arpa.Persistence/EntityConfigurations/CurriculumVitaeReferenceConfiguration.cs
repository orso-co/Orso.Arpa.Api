using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class CurriculumVitaeReferenceConfiguration : IEntityTypeConfiguration<CurriculumVitaeReference>
    {
        public void Configure(EntityTypeBuilder<CurriculumVitaeReference> builder)
        {
            builder
                .Property(a => a.TimeSpan)
                .HasMaxLength(50);

            builder
                .Property(a => a.Institution)
                .HasMaxLength(255);

            builder
                 .HasOne(a => a.Type)
                 .WithMany()
                 .HasForeignKey(a => a.TypeId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(a => a.Description)
                .HasMaxLength(500);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(m => m.CurriculumVitaeReferences)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
