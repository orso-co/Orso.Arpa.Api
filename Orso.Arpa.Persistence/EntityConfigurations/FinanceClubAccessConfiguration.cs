using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.FinanceDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class FinanceClubAccessConfiguration : IEntityTypeConfiguration<FinanceClubAccess>
    {
        public void Configure(EntityTypeBuilder<FinanceClubAccess> builder)
        {
            _ = builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder.HasOne(e => e.Club)
                .WithMany()
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder.HasIndex(e => new { e.UserId, e.ClubId }).IsUnique();

            _ = builder.Property(e => e.GrantedBy).HasMaxLength(200);
        }
    }
}
