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
        }
    }
}
