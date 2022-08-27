using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ProjectParticipationConfiguration : IEntityTypeConfiguration<ProjectParticipation>
    {
        public void Configure(EntityTypeBuilder<ProjectParticipation> builder)
        {
            builder
                .HasOne(e => e.Project)
                .WithMany(p => p.ProjectParticipations)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.MusicianProfile)
                .WithMany(m => m.ProjectParticipations)
                .HasForeignKey(e => e.MusicianProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.ParticipationStatusInner)
                .WithMany()
                .HasForeignKey(e => e.ParticipationStatusInnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.ParticipationStatusInternal)
                .WithMany()
                .HasForeignKey(e => e.ParticipationStatusInternalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.InvitationStatus)
                .WithMany()
                .HasForeignKey(e => e.InvitationStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.CommentByPerformerInner)
                .HasMaxLength(500);

            builder
                .Property(e => e.CommentByStaffInner)
                .HasMaxLength(500);
        }
    }
}
