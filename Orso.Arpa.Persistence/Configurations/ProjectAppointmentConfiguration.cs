using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class ProjectAppointmentConfiguration : IEntityTypeConfiguration<ProjectAppointment>
    {
        public void Configure(EntityTypeBuilder<ProjectAppointment> builder)
        {
            builder.HasKey(e => new { e.ProjectId, e.AppointmentId });
        }
    }
}
