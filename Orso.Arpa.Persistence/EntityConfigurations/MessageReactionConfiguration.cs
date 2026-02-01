using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
    {
        public void Configure(EntityTypeBuilder<MessageReaction> builder)
        {
            _ = builder
                .Property(e => e.Emoji)
                .HasMaxLength(50)
                .IsRequired();

            // Unique constraint: one reaction per user per message per emoji
            // (User can react with different emojis to the same message)
            _ = builder
                .HasIndex(e => new { e.MessageId, e.UserId, e.Emoji })
                .IsUnique();

            // Index for message lookups
            _ = builder.HasIndex(e => e.MessageId);

            // Relationship to ChatMessage
            _ = builder
                .HasOne(e => e.Message)
                .WithMany(m => m.Reactions)
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
