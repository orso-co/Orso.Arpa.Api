using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(e => e.GivenName)
                .HasMaxLength(50);

            builder
                .Property(e => e.Surname)
                .HasMaxLength(50);

            builder
                .Property(e => e.AboutMe)
                .HasMaxLength(1000);

            builder
                .HasData(PersonSeedData.Persons);
        }
    }
}
