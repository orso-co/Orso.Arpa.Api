using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
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
                .WithMany(m => m.Educations)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
