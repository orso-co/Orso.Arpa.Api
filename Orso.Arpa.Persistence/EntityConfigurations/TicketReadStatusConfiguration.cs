using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TicketReadStatusConfiguration : IEntityTypeConfiguration<TicketReadStatus>
    {
        public void Configure(EntityTypeBuilder<TicketReadStatus> builder)
        {
            _ = builder.HasIndex(e => new { e.TicketId, e.UserId }).IsUnique();

            _ = builder.HasOne(e => e.Ticket).WithMany(t => t.ReadStatuses).HasForeignKey(e => e.TicketId).OnDelete(DeleteBehavior.Cascade);
            _ = builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
