using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TicketMessageConfiguration : IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> builder)
        {
            _ = builder.Property(e => e.Content).HasMaxLength(4000).IsRequired();

            _ = builder.HasIndex(e => new { e.TicketId, e.SentAt });
            _ = builder.HasIndex(e => e.UserId);

            _ = builder.HasOne(e => e.Ticket).WithMany(t => t.Messages).HasForeignKey(e => e.TicketId).OnDelete(DeleteBehavior.Cascade);
            _ = builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
