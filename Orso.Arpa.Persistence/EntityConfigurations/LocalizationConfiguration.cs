using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.LocalizationDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class LocalizationConfiguration : IEntityTypeConfiguration<Localization>
    {
        public void Configure(EntityTypeBuilder<Localization> builder)
        {
            builder.HasAlternateKey(e => new {e.ResourceKey, e.LocalizationCulture, e.Key});

            builder.HasData(LocalizationSeedData.Localizations);

            builder.Property(m => m.Key).IsUnicode().HasMaxLength(1000).IsRequired();

            builder.Property(m => m.Text).IsUnicode().HasMaxLength(1000).IsRequired();

            builder.Property(m => m.LocalizationCulture).HasMaxLength(5).IsRequired();

            builder.Property(m => m.ResourceKey).HasMaxLength(50).IsRequired();
        }
    }
}
