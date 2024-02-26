using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ClubMembershipProfileDataConfiguration : IEntityTypeConfiguration<ClubMembershipProfileData>
    {
        public void Configure(EntityTypeBuilder<ClubMembershipProfileData> builder)
        {
            builder
                .HasOne(e => e.ClubMembershipProfile)
                .WithMany(p => p.MembershipHistory)
                .HasForeignKey(e => e.ClubMembershipProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.MembershipSubType)
                .WithMany()
                .HasForeignKey(e => e.MembershipSubTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey(e => e.BankAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.ReasonForDeviatingAnnualContribution)
                .HasMaxLength(500);  
        }
    }
}