using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PersonMembershipConfiguration : IEntityTypeConfiguration<PersonMembership>
    {
        public void Configure(EntityTypeBuilder<PersonMembership> builder)
        {
            // Currency field
            builder
                .Property(e => e.AnnualFee)
                .HasPrecision(10, 2);

            // Comment fields
            builder
                .Property(e => e.StaffComment)
                .HasMaxLength(500);

            builder
                .Property(e => e.PerformerComment)
                .HasMaxLength(500);

            // Person relationship (1:n)
            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.PersonMemberships)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            // SelectValueMapping relationships (enums)
            builder
                .HasOne(e => e.SupportLevel)
                .WithMany()
                .HasForeignKey(e => e.SupportLevelId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.MembershipStatus)
                .WithMany()
                .HasForeignKey(e => e.MembershipStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.PaymentMethod)
                .WithMany()
                .HasForeignKey(e => e.PaymentMethodId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.PaymentFrequency)
                .WithMany()
                .HasForeignKey(e => e.PaymentFrequencyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Club)
                .WithMany()
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
