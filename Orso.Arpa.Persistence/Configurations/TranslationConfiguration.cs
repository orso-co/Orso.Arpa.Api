using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.HasData(TranslationSeedData.Translations);

            builder.Property(m => m.Key).IsUnicode().HasMaxLength(1000).IsRequired();

            builder.Property(m => m.Text).IsUnicode().HasMaxLength(1000).IsRequired();

            builder.Property(m => m.LocalizationCulture).HasMaxLength(60).IsRequired();

            builder.Property(m => m.ResourceKey).HasMaxLength(50).IsRequired();

            builder.HasQueryFilter(m => !m.Deleted);
        }
    }
}
