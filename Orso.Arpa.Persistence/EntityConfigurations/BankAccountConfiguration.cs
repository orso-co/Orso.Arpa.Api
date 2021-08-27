using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder
            .Property(e => e.IBAN)
            .HasMaxLength(34);

            builder
            .Property(e => e.BIC)
            .HasMaxLength(11);

            builder
            .Property(e => e.CommentInner)
            .HasMaxLength(500);

            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.BankAccounts)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
