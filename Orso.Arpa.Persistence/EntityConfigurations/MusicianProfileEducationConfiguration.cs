using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MusicianProfileEducationConfiguration : IEntityTypeConfiguration<MusicianProfileEducation>
    {
        public void Configure(EntityTypeBuilder<MusicianProfileEducation> builder)
        {
            builder.HasKey(e => new { e.MusicianProfileId, e.EducationId });

            builder
                .HasOne(e => e.Education)
                .WithMany(r => r.MusicianProfileEducations)
                .HasForeignKey(e => e.EducationId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(r => r.MusicianProfileEducations)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
