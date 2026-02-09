using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.OrganizationDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            _ = builder
                .Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            _ = builder
                .Property(e => e.ShortName)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.Description)
                .HasMaxLength(1000);

            _ = builder
                .Property(e => e.Website)
                .HasMaxLength(500);

            _ = builder
                .Property(e => e.Email)
                .HasMaxLength(200);

            _ = builder
                .Property(e => e.Phone)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.Tags)
                .HasMaxLength(500);

            _ = builder
                .HasOne(e => e.LegalForm)
                .WithMany()
                .HasForeignKey(e => e.LegalFormId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.OrganizationType)
                .WithMany()
                .HasForeignKey(e => e.OrganizationTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasIndex(e => e.Name);
        }
    }
}
