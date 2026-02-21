using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            _ = builder.Property(e => e.FileName).HasMaxLength(500).IsRequired();
            _ = builder.Property(e => e.StoragePath).HasMaxLength(1000).IsRequired();
            _ = builder.Property(e => e.ContentType).HasMaxLength(200).IsRequired();

            _ = builder.Ignore(e => e.IsImage);

            _ = builder.HasIndex(e => e.TicketId);
            _ = builder.HasIndex(e => e.MessageId);

            _ = builder.HasOne(e => e.Ticket).WithMany(t => t.Attachments).HasForeignKey(e => e.TicketId).OnDelete(DeleteBehavior.Cascade);
            _ = builder.HasOne(e => e.Message).WithMany(m => m.Attachments).HasForeignKey(e => e.MessageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
