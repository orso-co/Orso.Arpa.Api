using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ClubMembershipSubTypeConfiguration : IEntityTypeConfiguration<ClubMembershipSubType>
    {
        public void Configure(EntityTypeBuilder<ClubMembershipSubType> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .Property(e => e.Advantages)
                .HasMaxLength(1000);

            builder
                .Property(e => e.Prerequisites)
                .HasMaxLength(1000);

            builder
                .HasOne(e => e.MembershipType)
                .WithMany(c => c.SubTypes)
                .HasForeignKey(e => e.MemberhsipTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasData(ClubMembershipSubTypeSeedData.ClubMembershipSubTypes);
        }
    }
}
