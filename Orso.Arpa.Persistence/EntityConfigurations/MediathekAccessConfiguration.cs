using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MediathekAccessConfiguration : IEntityTypeConfiguration<MediathekAccess>
    {
        public void Configure(EntityTypeBuilder<MediathekAccess> builder)
        {
            _ = builder
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(e => e.GrantedBy)
                .HasMaxLength(110);

            _ = builder
                .Property(e => e.Notes)
                .HasMaxLength(500);

            _ = builder.HasIndex(e => e.PersonId);
            _ = builder.HasIndex(e => e.IsActive);
        }
    }
}
