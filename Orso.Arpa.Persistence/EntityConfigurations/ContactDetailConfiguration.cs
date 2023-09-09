using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ContactDetailConfiguration : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            builder
                .Property(a => a.Value)
                .HasMaxLength(1000);

            builder
                .Property(a => a.CommentInner)
                .HasMaxLength(500);

            builder
                .Property(a => a.CommentTeam)
                .HasMaxLength(500);

            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.ContactDetails)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
