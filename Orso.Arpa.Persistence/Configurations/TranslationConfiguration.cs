using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.Property(m => m.Key).IsUnicode().IsRequired();

            builder.Property(m => m.Text).IsUnicode().IsRequired();

            builder.Property(m => m.LocalizationCulture).IsRequired();

            builder.Property(m => m.ResourceKey).IsRequired();

            builder.HasQueryFilter(m => !m.Deleted);
        }
    }
}
