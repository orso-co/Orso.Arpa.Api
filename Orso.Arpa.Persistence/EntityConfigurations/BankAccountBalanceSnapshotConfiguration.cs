using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.FinanceDomain.Enums;
using Orso.Arpa.Domain.FinanceDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class BankAccountBalanceSnapshotConfiguration : IEntityTypeConfiguration<BankAccountBalanceSnapshot>
    {
        public void Configure(EntityTypeBuilder<BankAccountBalanceSnapshot> builder)
        {
            _ = builder.Property(e => e.Balance).HasPrecision(18, 2).IsRequired();
            _ = builder.Property(e => e.AvailableBalance).HasPrecision(18, 2);
            _ = builder.Property(e => e.Currency).HasMaxLength(3).HasDefaultValue("EUR");
            _ = builder.Property(e => e.SyncStatus).HasConversion<string>().HasMaxLength(50).IsRequired();
            _ = builder.Property(e => e.ErrorMessage).HasMaxLength(2000);

            _ = builder.HasIndex(e => e.OrganizationBankAccountId);
            _ = builder.HasIndex(e => e.BalanceDate);
            _ = builder.HasIndex(e => e.SyncedAt);

            _ = builder.HasOne(e => e.OrganizationBankAccount)
                .WithMany(e => e.BalanceSnapshots)
                .HasForeignKey(e => e.OrganizationBankAccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
