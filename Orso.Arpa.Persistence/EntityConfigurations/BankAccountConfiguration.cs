using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain._General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder
                .Property(e => e.Iban)
                .HasMaxLength(34);

            builder
                .Property(e => e.Bic)
                .HasMaxLength(11);

            builder
                .Property(e => e.AccountOwner)
                .HasMaxLength(50);
        }
    }
}
