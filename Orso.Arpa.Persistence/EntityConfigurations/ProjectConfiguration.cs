using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            _ = builder
                .Property(e => e.Title)
                .HasMaxLength(100)
                .IsRequired();

            _ = builder
                .Property(e => e.ShortTitle)
                .HasMaxLength(30)
                .IsRequired();

            _ = builder
                .Property(e => e.Description)
                .HasMaxLength(1000);

            _ = builder
                .Property(e => e.Code)
                .HasMaxLength(15)
                .IsRequired();

            _ = builder
                .HasIndex(e => e.Code);

            _ = builder
                .HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Genre)
                .WithMany()
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .HasOne(e => e.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            _ = builder
                .Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(100);

            _ = builder.HasIndex(e => e.Status);
        }
    }
}
