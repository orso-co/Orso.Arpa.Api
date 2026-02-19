using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatLiveLocationShareConfiguration : IEntityTypeConfiguration<ChatLiveLocationShare>
    {
        public void Configure(EntityTypeBuilder<ChatLiveLocationShare> builder)
        {
            // Index for active shares lookup by room
            _ = builder.HasIndex(e => new { e.ChatRoomId, e.UserId, e.IsActive });

            // Index for expiry check
            _ = builder.HasIndex(e => e.ExpiresAt);

            // Relationship to ChatRoom
            _ = builder
                .HasOne(e => e.ChatRoom)
                .WithMany()
                .HasForeignKey(e => e.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to User
            _ = builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relationship to Message
            _ = builder
                .HasOne(e => e.Message)
                .WithMany()
                .HasForeignKey(e => e.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
