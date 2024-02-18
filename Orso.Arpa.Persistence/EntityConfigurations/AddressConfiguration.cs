using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain._General.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .Property(a => a.Address1)
                .HasMaxLength(100);

            builder
                .Property(a => a.Address2)
                .HasMaxLength(100);

            builder
                .Property(a => a.Zip)
                .HasMaxLength(20);

            builder
                .Property(a => a.City)
                .HasMaxLength(100);

            builder
                .Property(a => a.Country)
                .HasMaxLength(100);

            builder
                .Property(a => a.State)
                .HasMaxLength(100);

            builder
                .Property(a => a.UrbanDistrict)
                .HasMaxLength(100);

            builder
                .Property(a => a.CommentInner)
                .HasMaxLength(500);

            builder
                .HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
