using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations;

public class EmailUnsubscriptionConfiguration : IEntityTypeConfiguration<EmailUnsubscription>
{
    public void Configure(EntityTypeBuilder<EmailUnsubscription> builder)
    {
        _ = builder
            .Property(e => e.EmailAddress)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .Property(e => e.Reason)
            .HasMaxLength(500);

        _ = builder
            .HasIndex(e => e.PersonId);

        _ = builder
            .HasOne(e => e.Person)
            .WithMany()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
