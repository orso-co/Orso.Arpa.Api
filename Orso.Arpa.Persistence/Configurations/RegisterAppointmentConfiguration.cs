using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class RegisterAppointmentConfiguration : IEntityTypeConfiguration<RegisterAppointment>
    {
        public void Configure(EntityTypeBuilder<RegisterAppointment> builder)
        {
            builder.HasKey(e => new { e.RegisterId, e.AppointmentId });
        }
    }
}
