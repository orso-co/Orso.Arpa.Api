using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.HasData(TranslationSeedData.Translations);

            builder.Property(m => m.Key).IsUnicode().IsRequired();

            builder.Property(m => m.Text).IsUnicode().IsRequired();

            builder.Property(m => m.LocalizationCulture).IsRequired();

            builder.Property(m => m.ResourceKey).IsRequired();

        }
    }
}
