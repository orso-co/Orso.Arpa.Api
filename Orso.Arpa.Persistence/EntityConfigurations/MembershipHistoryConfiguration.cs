using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MembershipHistoryConfiguration : IEntityTypeConfiguration<MembershipHistory>
    {
        public void Configure(EntityTypeBuilder<MembershipHistory> builder)
        {
            // Currency field
            builder
                .Property(e => e.Amount)
                .HasPrecision(10, 2);

            // Comment field
            builder
                .Property(e => e.Comment)
                .HasMaxLength(500);

            // PersonMembership relationship (1:n)
            builder
                .HasOne(e => e.PersonMembership)
                .WithMany(m => m.MembershipHistories)
                .HasForeignKey(e => e.PersonMembershipId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
