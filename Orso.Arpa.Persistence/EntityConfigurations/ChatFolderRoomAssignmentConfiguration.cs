using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatFolderRoomAssignmentConfiguration : IEntityTypeConfiguration<ChatFolderRoomAssignment>
    {
        public void Configure(EntityTypeBuilder<ChatFolderRoomAssignment> builder)
        {
            _ = builder
                .Property(e => e.SortOrder)
                .IsRequired()
                .HasDefaultValue(0);

            // Unique constraint for system assignments (user_id IS NULL)
            _ = builder
                .HasIndex(e => new { e.FolderId, e.ChatRoomId, e.UserId })
                .IsUnique();

            // Index for folder lookups
            _ = builder.HasIndex(e => e.FolderId);

            // Index for room lookups
            _ = builder.HasIndex(e => e.ChatRoomId);

            // Index for user lookups
            _ = builder.HasIndex(e => e.UserId);

            // Relationship to Folder
            _ = builder
                .HasOne(e => e.Folder)
                .WithMany(f => f.RoomAssignments)
                .HasForeignKey(e => e.FolderId)
                .OnDelete(DeleteBehavior.Cascade);

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
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
