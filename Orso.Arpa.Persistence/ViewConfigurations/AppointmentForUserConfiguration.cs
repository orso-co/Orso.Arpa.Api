using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Views;

namespace Orso.Arpa.Persistence.ViewConfigurations
{
    public class AppointmentForUserConfiguration : IEntityTypeConfiguration<SqlFunctionResult>
    {
        public void Configure(EntityTypeBuilder<SqlFunctionResult> builder)
        {
            builder
                .ToTable("SqlFunctionResults", t => t.ExcludeFromMigrations());
        }
    }
}
