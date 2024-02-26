using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PersonBankAccountConfiguration : IEntityTypeConfiguration<PersonBankAccount>
    {
        public void Configure(EntityTypeBuilder<PersonBankAccount> builder)
        {
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
            
            builder
                .ComplexProperty(e => e.BankAccount)
                .IsRequired();
        }
    }
}
