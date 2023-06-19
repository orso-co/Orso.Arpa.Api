using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder
                .Property(e => e.NewsText)
                .HasMaxLength(1000);

            builder
                .Property(e => e.Url)
                .HasMaxLength(1000);
        }
    }
}
