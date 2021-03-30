using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder
                .Property(e => e.Href)
                .HasMaxLength(1000);

            builder
                .Property(e => e.AnchorText)
                .HasMaxLength(1000);

            builder
                .HasOne(e => e.Project)
                .WithMany(p => p.Urls)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
