using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            _ = builder
                .Property(e => e.Name)
                .HasMaxLength(200);

            _ = builder
                .Property(e => e.Type)
                .IsRequired();

            // Index for project lookups
            _ = builder.HasIndex(e => e.ProjectId);

            // Index for sorting by last message
            _ = builder.HasIndex(e => e.LastMessageAt);

            // Relationship to Project
            _ = builder
                .HasOne(e => e.Project)
                .WithMany()
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
