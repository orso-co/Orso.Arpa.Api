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
                .HasQueryFilter(u => !u.Deleted);

            builder
                .HasOne(e => e.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(e => e.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Ignore(e => e.DisplayName);
        }
    }
}
