using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileCurriculumVitaeReferenceConfiguration : IEntityTypeConfiguration<MusicianProfileCurriculumVitaeReference>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileCurriculumVitaeReference> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.CurriculumVitaeReferenceId });

            builder
                .HasOne(e => e.CurriculumVitaeReference)
                .WithMany(r => r.MusicianProfileCurriculumVitaeReferences)
                .HasForeignKey(e => e.CurriculumVitaeReferenceId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.MusicianProfileCurriculumVitaeReferences)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
