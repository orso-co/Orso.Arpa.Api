using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder
            .Property(e => e.Title)
            .HasMaxLength(200);

        builder
            .Property(e => e.Content)
            .HasMaxLength(1000);

        builder
            .Property(e => e.Url)
            .HasMaxLength(1000);
    }
}
