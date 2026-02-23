using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.FinanceDomain.Enums;
using Orso.Arpa.Domain.FinanceDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PendingTanRequestConfiguration : IEntityTypeConfiguration<PendingTanRequest>
    {
        public void Configure(EntityTypeBuilder<PendingTanRequest> builder)
        {
            _ = builder.Property(e => e.TanChallenge).HasMaxLength(1000);
            _ = builder.Property(e => e.TanMediumName).HasMaxLength(200);
            _ = builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
            _ = builder.Property(e => e.SubmittedTan).HasMaxLength(20);

            _ = builder.HasIndex(e => e.Status);
            _ = builder.HasIndex(e => e.OrganizationBankAccountId);

            _ = builder.HasOne(e => e.OrganizationBankAccount)
                .WithMany(e => e.TanRequests)
                .HasForeignKey(e => e.OrganizationBankAccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
