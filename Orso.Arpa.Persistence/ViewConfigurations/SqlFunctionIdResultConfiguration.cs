using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Persistence.ViewConfigurations
{
    public class SqlFunctionIdResultConfiguration : IEntityTypeConfiguration<SqlFunctionIdResult>
    {
        public void Configure(EntityTypeBuilder<SqlFunctionIdResult> builder)
        {
            builder
                .ToTable("SqlFunctionIdResults", t => t.ExcludeFromMigrations());
        }
    }
}
