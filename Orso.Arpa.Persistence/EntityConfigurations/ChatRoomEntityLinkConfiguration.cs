using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatRoomEntityLinkConfiguration : IEntityTypeConfiguration<ChatRoomEntityLink>
    {
        public void Configure(EntityTypeBuilder<ChatRoomEntityLink> builder)
        {
            _ = builder
                .Property(e => e.EntityType)
                .HasMaxLength(50)
                .IsRequired();

            _ = builder
                .Property(e => e.EntityDisplayName)
                .HasMaxLength(500);

            // 1:1 relationship: each ChatRoom can have at most one EntityLink
            _ = builder
                .HasIndex(e => e.ChatRoomId)
                .IsUnique();

            // Each entity can have at most one chat room
            _ = builder
                .HasIndex(e => new { e.EntityType, e.EntityId })
                .IsUnique();

            // FK to ChatRoom with cascade delete
            _ = builder
                .HasOne(e => e.ChatRoom)
                .WithOne(r => r.EntityLink)
                .HasForeignKey<ChatRoomEntityLink>(e => e.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
