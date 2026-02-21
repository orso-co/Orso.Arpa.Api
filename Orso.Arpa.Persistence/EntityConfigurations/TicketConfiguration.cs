using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.TicketDomain.Enums;
using Orso.Arpa.Domain.TicketDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            _ = builder.Property(e => e.Title).HasMaxLength(500).IsRequired();
            _ = builder.Property(e => e.Description).HasMaxLength(10000).IsRequired();

            _ = builder.Property(e => e.Type).HasConversion<string>().HasMaxLength(50).IsRequired();
            _ = builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(50).HasDefaultValue(TicketStatus.Open);
            _ = builder.Property(e => e.Effort).HasConversion<string>().HasMaxLength(50);

            _ = builder.HasIndex(e => e.Status);
            _ = builder.HasIndex(e => e.Type);
            _ = builder.HasIndex(e => e.CreatorId);

            _ = builder.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatorId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
