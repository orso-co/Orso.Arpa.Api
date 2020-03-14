using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class AppointmentParticipationConfiguration : IEntityTypeConfiguration<AppointmentParticipation>
    {
        public void Configure(EntityTypeBuilder<AppointmentParticipation> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.AppointmentParticipations)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Appointment)
                .WithMany(a => a.AppointmentParticipations)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Result)
                .WithMany(c => c.AppointmentParticipationsAsResult)
                .HasForeignKey(e => e.ResultId)
                .OnDelete(DeleteBehavior.SetNull);

            // ToDo: Set null in code
            builder
                .HasOne(e => e.Prediction)
                .WithMany(c => c.AppointmentParticipationsAsPrediction)
                .HasForeignKey(e => e.PredictionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
