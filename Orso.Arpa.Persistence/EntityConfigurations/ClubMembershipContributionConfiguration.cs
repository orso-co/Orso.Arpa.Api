using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ClubMembershipContributionConfiguration : IEntityTypeConfiguration<ClubMembershipContribution>
    {
        public void Configure(EntityTypeBuilder<ClubMembershipContribution> builder)
        {
            builder
                .HasOne(e => e.MembershipSubType)
                .WithMany(c => c.ContributionHistory)
                .HasForeignKey(e => e.MembershipSubTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasData(ClubMembershipContributionSeedData.ClubMembershipContributions);
        }
    }
}