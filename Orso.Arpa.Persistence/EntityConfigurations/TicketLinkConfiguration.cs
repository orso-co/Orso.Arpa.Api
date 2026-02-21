using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TicketLinkConfiguration : IEntityTypeConfiguration<TicketLink>
    {
        public void Configure(EntityTypeBuilder<TicketLink> builder)
        {
            _ = builder.Property(e => e.Label).HasMaxLength(500).IsRequired();
            _ = builder.Property(e => e.Url).HasMaxLength(2000).IsRequired();

            _ = builder.HasIndex(e => e.TicketId);

            _ = builder.HasOne(e => e.Ticket).WithMany(t => t.Links).HasForeignKey(e => e.TicketId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
