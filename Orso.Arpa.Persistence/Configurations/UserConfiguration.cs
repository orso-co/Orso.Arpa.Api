using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(e => e.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(e => e.PersonId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder
                .Ignore(e => e.DisplayName);
        }
    }
}
