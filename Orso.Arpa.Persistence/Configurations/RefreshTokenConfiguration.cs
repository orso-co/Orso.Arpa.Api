using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasOne(a => a.User)
                .WithMany(r => r.RefreshTokens)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(a => a.CreatedByIp)
                .HasMaxLength(50);

            builder
                .Property(a => a.RevokedByIp)
                .HasMaxLength(50);

            builder
                .Property(a => a.Token)
                .HasMaxLength(500);
        }
    }
}
