using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class EmailCampaignRecipientConfiguration : IEntityTypeConfiguration<EmailCampaignRecipient>
{
    public void Configure(EntityTypeBuilder<EmailCampaignRecipient> builder)
    {
        _ = builder
            .Property(e => e.EmailAddress)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.DisplayName)
            .HasMaxLength(200);

        _ = builder
            .Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        _ = builder
            .Property(e => e.ErrorMessage)
            .HasMaxLength(1000);

        _ = builder
            .HasIndex(e => e.TrackingToken)
            .IsUnique();

        _ = builder
            .HasIndex(e => new { e.EmailCampaignId, e.PersonId });

        _ = builder
            .HasOne(e => e.Person)
            .WithMany()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
