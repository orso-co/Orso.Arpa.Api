using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ChatDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ChatMessageAttachmentConfiguration : IEntityTypeConfiguration<ChatMessageAttachment>
    {
        public void Configure(EntityTypeBuilder<ChatMessageAttachment> builder)
        {
            _ = builder
                .Property(e => e.FileName)
                .HasMaxLength(500)
                .IsRequired();

            _ = builder
                .Property(e => e.StoragePath)
                .HasMaxLength(1000)
                .IsRequired();

            _ = builder
                .Property(e => e.ContentType)
                .HasMaxLength(200)
                .IsRequired();

            _ = builder
                .Property(e => e.ThumbnailPath)
                .HasMaxLength(1000);

            // Ignore computed properties
            _ = builder.Ignore(e => e.IsImage);
            _ = builder.Ignore(e => e.IsVideo);
            _ = builder.Ignore(e => e.IsAudio);
            _ = builder.Ignore(e => e.IsPdf);

            // Index for message lookups
            _ = builder.HasIndex(e => e.ChatMessageId);

            // Relationship to ChatMessage
            _ = builder
                .HasOne(e => e.ChatMessage)
                .WithMany(m => m.Attachments)
                .HasForeignKey(e => e.ChatMessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
