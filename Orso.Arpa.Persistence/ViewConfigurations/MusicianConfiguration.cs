using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Views;

namespace Orso.Arpa.Persistence.ViewConfigurations
{
    public class MusicianConfiguration : IEntityTypeConfiguration<Musician>
    {
        public void Configure(EntityTypeBuilder<Musician> builder)
        {
            builder
                .HasNoKey()
                .ToView("musician")
                .ToTable("musician", t => t.ExcludeFromMigrations());
        }
    }
}
