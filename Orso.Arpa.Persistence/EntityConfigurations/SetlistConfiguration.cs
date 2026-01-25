using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class SetlistConfiguration : IEntityTypeConfiguration<Setlist>
    {
        public void Configure(EntityTypeBuilder<Setlist> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(s => s.Description)
                .HasMaxLength(2000);

            builder.Property(s => s.IsTemplate)
                .HasDefaultValue(false);

            builder.HasIndex(s => s.Name);
            builder.HasIndex(s => s.IsTemplate);

            builder.HasMany(s => s.Pieces)
                .WithOne(sp => sp.Setlist)
                .HasForeignKey(sp => sp.SetlistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
