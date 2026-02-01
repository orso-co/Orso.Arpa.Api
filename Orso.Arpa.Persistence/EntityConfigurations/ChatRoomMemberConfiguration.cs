using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatRoomMemberConfiguration : IEntityTypeConfiguration<ChatRoomMember>
    {
        public void Configure(EntityTypeBuilder<ChatRoomMember> builder)
        {
            // Unique constraint: one user per chat room
            _ = builder
                .HasIndex(e => new { e.ChatRoomId, e.UserId })
                .IsUnique();

            // Relationship to ChatRoom
            _ = builder
                .HasOne(e => e.ChatRoom)
                .WithMany(r => r.Members)
                .HasForeignKey(e => e.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to User
            _ = builder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index for user lookups (find all chats for a user)
            _ = builder.HasIndex(e => e.UserId);
        }
    }
}
