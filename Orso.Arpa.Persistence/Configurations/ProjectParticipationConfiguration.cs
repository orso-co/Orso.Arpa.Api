using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class ProjectParticipationConfiguration : IEntityTypeConfiguration<ProjectParticipation>
    {
        public void Configure(EntityTypeBuilder<ProjectParticipation> builder)
        {
            builder
                .HasOne(e => e.Project)
                .WithMany(p => p.ProjectParticipations)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(m => m.ProjectParticipations)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
