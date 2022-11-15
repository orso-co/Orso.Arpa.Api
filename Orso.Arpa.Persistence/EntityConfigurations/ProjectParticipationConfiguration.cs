using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ProjectParticipationConfiguration : IEntityTypeConfiguration<ProjectParticipation>
    {
        public void Configure(EntityTypeBuilder<ProjectParticipation> builder)
        {
            _ = builder
                .HasOne(e => e.Project)
                .WithMany(p => p.ProjectParticipations)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(m => m.ProjectParticipations)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(e => e.CommentByPerformerInner)
                .HasMaxLength(500);

            _ = builder
                .Property(e => e.CommentByStaffInner)
                .HasMaxLength(500);

            _ = builder
                .Property(s => s.ParticipationStatusInner)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder
                .Property(s => s.ParticipationStatusInternal)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder
                .Property(s => s.InvitationStatus)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder.HasIndex(e => e.ParticipationStatusInner);

            _ = builder.HasIndex(e => e.ParticipationStatusInternal);

            _ = builder.HasIndex(e => e.InvitationStatus);
        }
    }
}
