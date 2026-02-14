using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class EmailCampaignConfiguration : IEntityTypeConfiguration<EmailCampaign>
{
    public void Configure(EntityTypeBuilder<EmailCampaign> builder)
    {
        _ = builder
            .Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.Subject)
            .HasMaxLength(500)
            .IsRequired();

        _ = builder
            .Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        _ = builder
            .HasIndex(e => e.Status);

        _ = builder
            .HasOne(e => e.EmailTemplate)
            .WithMany()
            .HasForeignKey(e => e.EmailTemplateId)
            .OnDelete(DeleteBehavior.NoAction);

        _ = builder
            .HasMany(e => e.Recipients)
            .WithOne(r => r.EmailCampaign)
            .HasForeignKey(r => r.EmailCampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder
            .HasMany(e => e.Attachments)
            .WithOne(a => a.EmailCampaign)
            .HasForeignKey(a => a.EmailCampaignId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
