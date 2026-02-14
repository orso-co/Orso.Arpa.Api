using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class EmailCampaignAttachmentConfiguration : IEntityTypeConfiguration<EmailCampaignAttachment>
{
    public void Configure(EntityTypeBuilder<EmailCampaignAttachment> builder)
    {
        _ = builder
            .Property(e => e.FileName)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.ContentType)
            .HasMaxLength(100)
            .IsRequired();

        _ = builder
            .Property(e => e.StoragePath)
            .HasMaxLength(500)
            .IsRequired();
    }
}
