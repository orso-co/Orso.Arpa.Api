using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            _ = builder
                .Property(e => e.Content)
                .HasMaxLength(4000)
                .IsRequired();

            // Index for chat room message lookups (with time ordering)
            _ = builder.HasIndex(e => new { e.ChatRoomId, e.SentAt });

            // Index for sender lookups
            _ = builder.HasIndex(e => e.SenderId);

            // Relationship to ChatRoom
            _ = builder
                .HasOne(e => e.ChatRoom)
                .WithMany(r => r.Messages)
                .HasForeignKey(e => e.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to Sender (User)
            _ = builder
                .HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            // Self-referential relationship for replies
            _ = builder
                .HasOne(e => e.ReplyToMessage)
                .WithMany()
                .HasForeignKey(e => e.ReplyToMessageId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
