using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
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
                .HasMany(e => e.UserRoles)
                .WithOne(p => p.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder
                .Ignore(e => e.DisplayName);
        }
    }
}
