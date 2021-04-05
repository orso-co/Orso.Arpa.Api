using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class SectionAppointmentConfiguration : IEntityTypeConfiguration<SectionAppointment>
    {
        public void Configure(EntityTypeBuilder<SectionAppointment> builder)
        {
            builder.HasKey(e => new { e.SectionId, e.AppointmentId });

            builder
                .HasOne(e => e.Section)
                .WithMany(s => s.SectionAppointments)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Appointment)
                .WithMany(a => a.SectionAppointments)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
