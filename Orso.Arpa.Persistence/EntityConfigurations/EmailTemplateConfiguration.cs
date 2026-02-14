using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        _ = builder
            .Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.Description)
            .HasMaxLength(500);

        _ = builder
            .Property(e => e.ThumbnailPath)
            .HasMaxLength(500);
    }
}
