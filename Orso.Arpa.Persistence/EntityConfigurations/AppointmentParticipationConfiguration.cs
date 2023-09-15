using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AppointmentDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AppointmentParticipationConfiguration : IEntityTypeConfiguration<AppointmentParticipation>
    {
        public void Configure(EntityTypeBuilder<AppointmentParticipation> builder)
        {
            _ = builder
                .HasOne(e => e.Person)
                .WithMany(p => p.AppointmentParticipations)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Appointment)
                .WithMany(a => a.AppointmentParticipations)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(e => e.CommentByPerformerInner)
                .HasMaxLength(500);

            _ = builder
                .Property(s => s.Result)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder
                .Property(s => s.Prediction)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder.HasIndex(e => e.Result);

            _ = builder.HasIndex(e => e.Prediction);
        }
    }
}
