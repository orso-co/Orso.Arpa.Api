using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class CredentialConfiguration : IEntityTypeConfiguration<Credential>
    {
        public void Configure(EntityTypeBuilder<Credential> builder)
        {
            builder
                .Property(a => a.Timespan)
                .HasMaxLength(50);

            builder
                .Property(a => a.Keyword)
                .HasMaxLength(50);

            builder
                .Property(a => a.Details)
                .HasMaxLength(250);
        }
    }
}
