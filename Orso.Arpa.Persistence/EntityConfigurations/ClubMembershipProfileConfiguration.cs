using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ClubMembershipProfileConfiguration : IEntityTypeConfiguration<ClubMembershipProfile>
    {
        public void Configure(EntityTypeBuilder<ClubMembershipProfile> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.ClubMemberships)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Club)
                .WithMany(c => c.Members)
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(e => e.ReasonForDeviatingMembershipTerminationDate)
                .HasMaxLength(500);

            builder
                .Property(e => e.TerminationReason)
                .HasMaxLength(500);
        }
    }
}