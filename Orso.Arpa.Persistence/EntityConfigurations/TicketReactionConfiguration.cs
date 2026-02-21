using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TicketReactionConfiguration : IEntityTypeConfiguration<TicketReaction>
    {
        public void Configure(EntityTypeBuilder<TicketReaction> builder)
        {
            _ = builder.Property(e => e.Emoji).HasMaxLength(50).IsRequired();

            _ = builder.HasIndex(e => new { e.MessageId, e.UserId, e.Emoji }).IsUnique();

            _ = builder.HasOne(e => e.Message).WithMany(m => m.Reactions).HasForeignKey(e => e.MessageId).OnDelete(DeleteBehavior.Cascade);
            _ = builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
