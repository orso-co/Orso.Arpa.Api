using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

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
                .Property(e => e.BirthName)
                .HasMaxLength(50);

            builder
                .Property(e => e.AboutMe)
                .HasMaxLength(1000);

            builder
                .Property(e => e.Birthplace)
                .HasMaxLength(50);

            builder
                .Property(e => e.Background)
                .HasMaxLength(500);

            builder
                .Property(e => e.MovingBox)
                .HasMaxLength(10000);

            builder
                .HasOne(e => e.Gender)
                .WithMany()
                .HasForeignKey(e => e.GenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(e => e.ContactVia)
               .WithMany(e => e.ContactsRecommended)
               .HasForeignKey(e => e.ContactViaId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
