using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.FinanceDomain.Enums;
using Orso.Arpa.Domain.FinanceDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class OrganizationBankAccountConfiguration : IEntityTypeConfiguration<OrganizationBankAccount>
    {
        public void Configure(EntityTypeBuilder<OrganizationBankAccount> builder)
        {
            _ = builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
            _ = builder.Property(e => e.Iban).HasMaxLength(34);
            _ = builder.Property(e => e.Bic).HasMaxLength(11);
            _ = builder.Property(e => e.BankName).HasMaxLength(200);
            _ = builder.Property(e => e.AccountType).HasConversion<string>().HasMaxLength(50).IsRequired();
            _ = builder.Property(e => e.EncryptedFinTsCredentials).HasMaxLength(2000);
            _ = builder.Property(e => e.EncryptedPayPalCredentials).HasMaxLength(2000);

            _ = builder.HasIndex(e => e.AccountType);
            _ = builder.HasIndex(e => e.IsActive);

            _ = builder.HasOne(e => e.Club)
                .WithMany()
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.SetNull);

            _ = builder.HasIndex(e => e.ClubId);
        }
    }
}
