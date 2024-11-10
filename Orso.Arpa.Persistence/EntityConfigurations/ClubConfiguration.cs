using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(50);

            builder
                .HasData(ClubSeedData.Clubs);

           /* builder
                .ComplexProperty(e => e.Address)
                .IsRequired(); */
        }
    }
}
