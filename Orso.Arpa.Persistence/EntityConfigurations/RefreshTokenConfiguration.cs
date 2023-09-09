using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .HasOne(a => a.User)
                .WithMany(r => r.RefreshTokens)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

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
