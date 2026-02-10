using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.OrganizationDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PersonOrganizationConfiguration : IEntityTypeConfiguration<PersonOrganization>
    {
        public void Configure(EntityTypeBuilder<PersonOrganization> builder)
        {
            _ = builder
                .Property(e => e.Function)
                .HasMaxLength(200);

            _ = builder
                .HasOne(e => e.Person)
                .WithMany(p => p.PersonOrganizations)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder
                .HasOne(e => e.Organization)
                .WithMany(o => o.PersonOrganizations)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder
                .HasOne(e => e.RelationshipType)
                .WithMany()
                .HasForeignKey(e => e.RelationshipTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasIndex(e => new { e.PersonId, e.OrganizationId, e.RelationshipTypeId })
                .IsUnique()
                .HasFilter(null);
        }
    }
}
