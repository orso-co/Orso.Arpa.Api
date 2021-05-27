using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class CurriculumVitaeReferenceConfiguration : IEntityTypeConfiguration<CurriculumVitaeReference>
    {
        public void Configure(EntityTypeBuilder<CurriculumVitaeReference> builder)
        {
            builder
                .Property(a => a.Timespan)
                .HasMaxLength(50);

            builder
                .Property(a => a.Keyword)
                .HasMaxLength(50);

            builder
                .Property(a => a.Details)
                .HasMaxLength(250);
        }
    }
}
