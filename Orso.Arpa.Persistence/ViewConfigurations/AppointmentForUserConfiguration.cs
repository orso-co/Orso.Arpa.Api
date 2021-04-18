using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Views;

namespace Orso.Arpa.Persistence.ViewConfigurations
{
    public class AppointmentForUserConfiguration : IEntityTypeConfiguration<AppointmentForUser>
    {
        public void Configure(EntityTypeBuilder<AppointmentForUser> builder)
        {
            builder
                .HasNoKey()
                .ToView("appointments_for_user")
                .ToTable("appointments_for_user", t => t.ExcludeFromMigrations());
        }
    }
}
