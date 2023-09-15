using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PersonSectionConfiguration : IEntityTypeConfiguration<PersonSection>
    {
        public void Configure(EntityTypeBuilder<PersonSection> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithMany(r => r.StakeholderGroups)
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .HasOne(e => e.Section)
                .WithMany(r => r.StakeholderGroups)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
