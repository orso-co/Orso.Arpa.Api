using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder
                .Property(a => a.Timespan)
                .HasMaxLength(50);

            builder
                .Property(a => a.Institution)
                .HasMaxLength(50);

            builder
                .Property(a => a.Comment)
                .HasMaxLength(500);
        }
    }
}
