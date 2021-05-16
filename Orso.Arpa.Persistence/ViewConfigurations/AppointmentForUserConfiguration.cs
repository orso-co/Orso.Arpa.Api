using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Views;

namespace Orso.Arpa.Persistence.ViewConfigurations
{
    public class AppointmentForUserConfiguration : IEntityTypeConfiguration<AppointmentForPerson>
    {
        public void Configure(EntityTypeBuilder<AppointmentForPerson> builder)
        {
            builder
                .ToTable("AppointmentForUser", t => t.ExcludeFromMigrations());
        }
    }
}
