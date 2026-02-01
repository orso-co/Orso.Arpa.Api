using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MessageReadReceiptConfiguration : IEntityTypeConfiguration<MessageReadReceipt>
    {
        public void Configure(EntityTypeBuilder<MessageReadReceipt> builder)
        {
            // Unique constraint: one read receipt per user per message
            _ = builder
                .HasIndex(e => new { e.MessageId, e.UserId })
                .IsUnique();

            // Index for message lookups
            _ = builder.HasIndex(e => e.MessageId);

            // Relationship to ChatMessage
            _ = builder
                .HasOne(e => e.Message)
                .WithMany(m => m.ReadReceipts)
                .HasForeignKey(e => e.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to User
            _ = builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
