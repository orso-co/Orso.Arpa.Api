using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AuditionConfiguration : IEntityTypeConfiguration<Audition>
    {
        public void Configure(EntityTypeBuilder<Audition> builder)
        {
            builder
                .HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.RepetitorStatus)
                .WithMany()
                .HasForeignKey(e => e.RepetitorStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Appointment)
                .WithOne(p => p.Audition)
                .HasForeignKey<Audition>(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .Property(e => e.InnerComment)
                .HasMaxLength(500);

            builder
                .Property(e => e.InternalComment)
                .HasMaxLength(500);

            builder
                .Property(e => e.Repertoire)
                .HasMaxLength(500);
        }
    }
}
