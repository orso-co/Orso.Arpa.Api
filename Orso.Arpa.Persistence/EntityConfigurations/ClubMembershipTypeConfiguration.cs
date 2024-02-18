using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ClubMembershipTypeConfiguration : IEntityTypeConfiguration<ClubMembershipType>
    {
        public void Configure(EntityTypeBuilder<ClubMembershipType> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(50);
            
            builder
                .Property(e => e.Description)
                .HasMaxLength(1000);

            builder
                .HasOne(e => e.Club)
                .WithMany(c => c.MembershipTypes)
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasData(ClubMembershipTypeSeedData.ClubMembershipTypes);
        }
    }
}