using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ProjectAppointmentConfiguration : IEntityTypeConfiguration<ProjectAppointment>
    {
        public void Configure(EntityTypeBuilder<ProjectAppointment> builder)
        {
            builder.HasKey(e => new { e.ProjectId, e.AppointmentId });

            builder
                .HasOne(e => e.Project)
                .WithMany(p => p.ProjectAppointments)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Appointment)
                .WithMany(p => p.ProjectAppointments)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
