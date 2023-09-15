using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            _ = builder
                .Property(e => e.GivenName)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.Surname)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.BirthName)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.AboutMe)
                .HasMaxLength(1000);

            _ = builder
                .Property(e => e.Birthplace)
                .HasMaxLength(50);

            _ = builder
                .Property(e => e.PersonBackgroundTeam)
                .HasMaxLength(500);

            _ = builder
                .Property(e => e.MovingBox)
                .HasMaxLength(10000);

            _ = builder
                .Property(e => e.ProfilePictureFileName)
                .HasMaxLength(100);

            _ = builder
                .HasOne(e => e.Gender)
                .WithMany()
                .HasForeignKey(e => e.GenderId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
               .HasOne(e => e.ContactVia)
               .WithMany(e => e.ContactsRecommended)
               .HasForeignKey(e => e.ContactViaId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
