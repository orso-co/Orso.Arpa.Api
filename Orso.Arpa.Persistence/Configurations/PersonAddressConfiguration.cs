using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class PersonAddressConfiguration : IEntityTypeConfiguration<PersonAddress>
    {
        public void Configure(EntityTypeBuilder<PersonAddress> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithMany(p => p.Addresses)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Type)
                .WithMany(s => s.PersonAddresses)
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
