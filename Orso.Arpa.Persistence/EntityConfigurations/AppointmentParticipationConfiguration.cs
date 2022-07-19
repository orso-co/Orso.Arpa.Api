using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AppointmentParticipationConfiguration : IEntityTypeConfiguration<AppointmentParticipation>
    {
        public void Configure(EntityTypeBuilder<AppointmentParticipation> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.AppointmentParticipations)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Appointment)
                .WithMany(a => a.AppointmentParticipations)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Result)
                .WithMany()
                .HasForeignKey(e => e.ResultId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Prediction)
                .WithMany()
                .HasForeignKey(e => e.PredictionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.CommentByPerformerInner)
                .HasMaxLength(500);
        }
    }
}
