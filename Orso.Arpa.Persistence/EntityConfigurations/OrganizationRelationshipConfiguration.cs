using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.OrganizationDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class OrganizationRelationshipConfiguration : IEntityTypeConfiguration<OrganizationRelationship>
    {
        public void Configure(EntityTypeBuilder<OrganizationRelationship> builder)
        {
            _ = builder
                .Property(e => e.Description)
                .HasMaxLength(500);

            _ = builder
                .HasOne(e => e.SourceOrganization)
                .WithMany(o => o.RelationshipsAsSource)
                .HasForeignKey(e => e.SourceOrganizationId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.TargetOrganization)
                .WithMany(o => o.RelationshipsAsTarget)
                .HasForeignKey(e => e.TargetOrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = builder
                .HasOne(e => e.RelationshipType)
                .WithMany()
                .HasForeignKey(e => e.RelationshipTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasIndex(e => new { e.SourceOrganizationId, e.TargetOrganizationId, e.RelationshipTypeId })
                .IsUnique()
                .HasFilter(null);

            _ = builder
                .ToTable(t => t.HasCheckConstraint("CK_OrganizationRelationship_NoSelfReference",
                    "\"source_organization_id\" <> \"target_organization_id\""));
        }
    }
}
