using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.NewsDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class NewsReadStatusConfiguration : IEntityTypeConfiguration<NewsReadStatus>
{
    public void Configure(EntityTypeBuilder<NewsReadStatus> builder)
    {
        _ = builder
            .HasIndex(e => new { e.NewsId, e.UserId })
            .IsUnique();

        _ = builder.HasIndex(e => e.NewsId);

        _ = builder
            .HasOne(e => e.News)
            .WithMany()
            .HasForeignKey(e => e.NewsId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
