using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MediathekAccessRequestConfiguration : IEntityTypeConfiguration<MediathekAccessRequest>
    {
        public void Configure(EntityTypeBuilder<MediathekAccessRequest> builder)
        {
            _ = builder
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder
                .Property(e => e.ProcessedBy)
                .HasMaxLength(110);

            _ = builder
                .Property(e => e.Message)
                .HasMaxLength(1000);

            _ = builder.HasIndex(e => e.PersonId);
            _ = builder.HasIndex(e => e.Status);
        }
    }
}
